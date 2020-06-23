using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using ContentsLimit.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace ContentsLimit.Business
{
    public class DataContext : DbContext
    {
        private readonly string _connectionString;
        public DataContext(IConfiguration configuration, DbContextOptions<DataContext> options) : base(options)
        {
            // A constructor is injected to a controller even if it doesnt get used by the action, 
            // this can make it seem like 2 contexts are created on screens with telerik grids if the action doesnt use its injected context but the grid then makes an ajax request which hits this line again.
            Debug.WriteLine("DBContext created via DI");
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        /// <summary>
        /// Constructor used by non standard contexts and unit tests.
        /// Theres no easy way to construct a context without DI, ie: from a static business tier class or unit test.
        /// An issue still with this method is because the DbContextOptions are not passed through that any queries run via a context created using this constructor will not output to the debug window.
        /// Because we configure our context completely in the OnConfiguring method, all the other configuration should be the same using either constructor.
        /// </summary>
        public DataContext(string connectionString)
        {
            Debug.WriteLine("DBContext created manually");
            _connectionString = connectionString;
        }

		public DbSet<Category> Categories { get; set; }
		public DbSet<ContentItem> Items { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
            optionsBuilder.UseSqlServer(_connectionString)
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .EnableSensitiveDataLogging(); // We need the EnableSensitiveDataLogging() or the params all show as ? in debug output
        }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			Debug.WriteLine($"{nameof(DataContext)} -> OnModelCreating (provider = {Database.ProviderName})");

			base.OnModelCreating(modelBuilder);
		}

		public override int SaveChanges()
		{
			Debug.WriteLine("called -> SaveChanges");
			try
			{
				var changes = base.SaveChanges();
				Debug.WriteLine($"{changes} changes saved.");
				Debug.WriteIf(changes == 0, "If changes were expected, verify AsTracking() was used on the query.");
				return changes;
			}
			catch (Exception ex)
			{
				throw HandleEntityFrameworkExceptions(ex);
			};
		}

		public override async Task<int> SaveChangesAsync(CancellationToken ct = new CancellationToken())
		{
			Debug.WriteLine("called -> SaveChangesAsync");
			try
			{
				var changes = await base.SaveChangesAsync(ct);
				Debug.WriteLine($"{changes} changes saved.");
				Debug.WriteLineIf(changes == 0, "If changes were expected, verify AsTracking() was used on the query.");
				return changes;
			}
			catch (Exception ex)
			{
				throw HandleEntityFrameworkExceptions(ex);
			}
		}

        /// <summary>Re-throw some Entity Framework exceptions as ValidationException so the UI can handle them.</summary>
        private static Exception HandleEntityFrameworkExceptions(Exception ex)
        {
            if (ex is DbUpdateConcurrencyException) return new ConcurrencyException(ex);
            if (ex is DbUpdateException)
            {
                // DbUpdateException could be used to retrieve EF data that caused the exception
                if (ex.GetBaseException() is SqlException sqlException)
                {
                    switch (sqlException.Number)
                    {
                        case 547:
                            return new ForeignKeyConstraintException();
                        case 2601:
                            return new UniqueConstraintException(sqlException);
                        case 2627:
                            return new PrimaryKeyConstraintException();
                    }
                }
            }
            return ex;
        }

    }
}
