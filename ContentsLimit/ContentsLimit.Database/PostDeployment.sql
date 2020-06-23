/*
Post-Deployment Script:
Publisher knows to run this after publishes because of how its included in the .sqlproj file like this: <PostDeploy Include="PostDeployment.sql" />
*/
SET IDENTITY_INSERT [Categories] ON;
INSERT INTO dbo.Categories ([Id], [Name]) VALUES (1, 'Clothing');
INSERT INTO dbo.Categories ([Id], [Name]) VALUES (2, 'Electronics');
INSERT INTO dbo.Categories ([Id], [Name]) VALUES (3, 'Kitchen');
INSERT INTO dbo.Categories ([Id], [Name]) VALUES (4, 'Sports');
SET IDENTITY_INSERT [Categories] OFF;
GO
INSERT INTO dbo.Items (CategoryId, [Name], Cost) VALUES (1, 'T-shirt', 30);
INSERT INTO dbo.Items (CategoryId, [Name], Cost) VALUES (1, 'Jeans', 30);
INSERT INTO dbo.Items (CategoryId, [Name], Cost) VALUES (4, 'Snowboard', 400);
INSERT INTO dbo.Items (CategoryId, [Name], Cost) VALUES (4, 'Bike', 300);