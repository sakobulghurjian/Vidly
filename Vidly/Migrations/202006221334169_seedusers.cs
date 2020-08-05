namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seedusers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'637d7865-e99e-45ed-b91e-4b23e8252953', N'admin@vidly.com', 0, N'ALqbNbs56FA4WYwqmVEkbybvkZkj0VnePar1tdzCoA3DG+Dud8joEqddWP/Rxc7aVQ==', N'2c39c6ff-47d5-4a7a-836b-5f48bce1e7b0', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'c6dd31e1-e2a5-4b10-a049-68ba3c7d21ae', N'guest@Vidly.com', 0, N'AJ+F9HmNHty3IuYCsQl/sbkGSig3XRSYpRTZ7YhxSRhmv7eI7fz5GUbAWPNQXnJV8w==', N'731bc921-b20d-47d9-af48-fdc1b285bc80', NULL, 0, 0, NULL, 1, 0, N'guest@Vidly.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'dd431dee-3621-4905-b45d-2ad961137fe4', N'CanManageMovies')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'637d7865-e99e-45ed-b91e-4b23e8252953', N'dd431dee-3621-4905-b45d-2ad961137fe4')
");
        }
        
        public override void Down()
        {
        }
    }
}
