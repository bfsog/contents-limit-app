#nullable enable
using System;
using Microsoft.Data.SqlClient;

namespace ContentsLimit.Business
{
	/// <summary>Use for exceptions that are expected. Allows screens to catch and re-display a nicer message if desired.</summary>
	public class ValidationException : ApplicationException
	{
		public ValidationException(string message) : base(message) { }
		public ValidationException(string message, Exception innerException) : base(message, innerException) { }
	}

	/// <summary>Database foreign key constraint exception. Referential integrity violation.</summary>
	public class ForeignKeyConstraintException : ValidationException
	{
		public ForeignKeyConstraintException() : base("Unable to insert with invalid relationships or delete an object because it has children.") { }
	}

	/// <summary>Database primary key constraint exception. Results from attempting to insert a duplicate primary key value.</summary>
	public class PrimaryKeyConstraintException : ValidationException
	{
		public PrimaryKeyConstraintException() : base("Unable to insert a duplicate primary key value.") { }
	}

	/// <summary>Entity Framework concurrency exception. Can result when one user loads a view to edit a record and the record is deleted before the edit.</summary>
	public class ConcurrencyException : ValidationException
	{
		public ConcurrencyException(Exception ex) : base("Concurrency error. Data has been deleted or modified.", ex) { }
	}

	/// <summary>Database unique constraint exception. Violation of a unique or primary key index.</summary>
	public class UniqueConstraintException : ValidationException
	{
		public UniqueConstraintException(SqlException ex) : base(ex.Message, ex) { } //this results in an ugly message but makes the problem obvious for the user by displaying the rejected key value
	}
}
