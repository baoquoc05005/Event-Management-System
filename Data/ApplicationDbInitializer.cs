using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EventManagementSystemFinal.Models;

namespace EventManagementSystemFinal.Data
{
    public class ApplicationDbInitializer
    {
        public static async Task Initialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Create database
            await context.Database.EnsureCreatedAsync();

            // Create roles
            await CreateRoles(roleManager);

            // Create admin user
            await CreateAdminUser(userManager, roleManager);

            // Seed categories
            await SeedCategories(context);

            // Seed events
            await SeedEvents(context);

            await context.SaveChangesAsync();
        }

        private static async Task CreateRoles(RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = { "Admin", "User" };

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }

        private static async Task CreateAdminUser(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var adminUser = await userManager.FindByEmailAsync("admin@eventmanagement.com");

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = "admin@eventmanagement.com",
                    Email = "admin@eventmanagement.com",
                    FirstName = "Admin",
                    LastName = "User",
                    EmailConfirmed = true,
                    CreatedAt = DateTime.Now
                };

                var result = await userManager.CreateAsync(adminUser, "Admin123!");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }

        private static async Task SeedCategories(ApplicationDbContext context)
        {
            if (!await context.Categories.AnyAsync())
            {
                var categories = new[]
                {
                    new Category { Name = "Technology", Description = "Tech conferences, workshops, and seminars", CreatedAt = DateTime.Now },
                    new Category { Name = "Business", Description = "Business networking and professional development", CreatedAt = DateTime.Now },
                    new Category { Name = "Education", Description = "Educational workshops and training sessions", CreatedAt = DateTime.Now },
                    new Category { Name = "Entertainment", Description = "Concerts, shows, and cultural events", CreatedAt = DateTime.Now },
                    new Category { Name = "Sports", Description = "Sports events and fitness activities", CreatedAt = DateTime.Now }
                };

                await context.Categories.AddRangeAsync(categories);
            }
        }

        private static async Task SeedEvents(ApplicationDbContext context)
        {
            if (!await context.Events.AnyAsync())
            {
                var categories = await context.Categories.ToListAsync();
                var events = new[]
                {
                    new Event 
                    { 
                        Title = "AI & Machine Learning Summit", 
                        Description = "Explore the latest trends in artificial intelligence and machine learning with industry experts.",
                        Location = "Tech Convention Center", 
                        EventDate = DateTime.Now.AddDays(30), 
                        Capacity = 500,
                        CategoryId = categories.FirstOrDefault(c => c.Name == "Technology")?.CategoryId,
                        CreatedAt = DateTime.Now
                    },
                    new Event 
                    { 
                        Title = "Startup Networking Night", 
                        Description = "Connect with entrepreneurs, investors, and innovators in the startup ecosystem.",
                        Location = "Innovation Hub", 
                        EventDate = DateTime.Now.AddDays(15), 
                        Capacity = 100,
                        CategoryId = categories.FirstOrDefault(c => c.Name == "Business")?.CategoryId,
                        CreatedAt = DateTime.Now
                    },
                    new Event 
                    { 
                        Title = "Web Development Bootcamp", 
                        Description = "Intensive 3-day bootcamp covering modern web development technologies and best practices.",
                        Location = "Digital Learning Center", 
                        EventDate = DateTime.Now.AddDays(45), 
                        Capacity = 50,
                        CategoryId = categories.FirstOrDefault(c => c.Name == "Education")?.CategoryId,
                        CreatedAt = DateTime.Now
                    },
                    new Event 
                    { 
                        Title = "Jazz & Blues Festival", 
                        Description = "An evening of smooth jazz and blues featuring renowned musicians from around the world.",
                        Location = "City Amphitheater", 
                        EventDate = DateTime.Now.AddDays(60), 
                        Capacity = 2000,
                        CategoryId = categories.FirstOrDefault(c => c.Name == "Entertainment")?.CategoryId,
                        CreatedAt = DateTime.Now
                    },
                    new Event 
                    { 
                        Title = "Marathon Training Workshop", 
                        Description = "Professional coaching and training for marathon runners of all levels.",
                        Location = "Sports Complex", 
                        EventDate = DateTime.Now.AddDays(20), 
                        Capacity = 75,
                        CategoryId = categories.FirstOrDefault(c => c.Name == "Sports")?.CategoryId,
                        CreatedAt = DateTime.Now
                    },
                    new Event 
                    { 
                        Title = "Cloud Computing Conference", 
                        Description = "Deep dive into cloud architecture, DevOps, and scalable infrastructure solutions.",
                        Location = "Tech Convention Center", 
                        EventDate = DateTime.Now.AddDays(90), 
                        Capacity = 300,
                        CategoryId = categories.FirstOrDefault(c => c.Name == "Technology")?.CategoryId,
                        CreatedAt = DateTime.Now
                    },
                    new Event 
                    { 
                        Title = "Marketing Strategies Summit", 
                        Description = "Learn cutting-edge marketing strategies from industry leaders and successful entrepreneurs.",
                        Location = "Business Center", 
                        EventDate = DateTime.Now.AddDays(75), 
                        Capacity = 150,
                        CategoryId = categories.FirstOrDefault(c => c.Name == "Business")?.CategoryId,
                        CreatedAt = DateTime.Now
                    },
                    new Event 
                    { 
                        Title = "Data Science Workshop", 
                        Description = "Hands-on workshop covering data analysis, visualization, and machine learning fundamentals.",
                        Location = "Tech Lab", 
                        EventDate = DateTime.Now.AddDays(10), 
                        Capacity = 40,
                        CategoryId = categories.FirstOrDefault(c => c.Name == "Technology")?.CategoryId,
                        CreatedAt = DateTime.Now
                    },
                    new Event 
                    { 
                        Title = "Photography Masterclass", 
                        Description = "Professional photography techniques and post-processing skills for enthusiasts.",
                        Location = "Art Gallery", 
                        EventDate = DateTime.Now.AddDays(35), 
                        Capacity = 25,
                        CategoryId = categories.FirstOrDefault(c => c.Name == "Entertainment")?.CategoryId,
                        CreatedAt = DateTime.Now
                    },
                    new Event 
                    { 
                        Title = "Yoga & Wellness Retreat", 
                        Description = "A day of yoga, meditation, and wellness activities for mind and body balance.",
                        Location = "Wellness Center", 
                        EventDate = DateTime.Now.AddDays(50), 
                        Capacity = 60,
                        CategoryId = categories.FirstOrDefault(c => c.Name == "Sports")?.CategoryId,
                        CreatedAt = DateTime.Now
                    },
                    new Event 
                    { 
                        Title = "Blockchain & Cryptocurrency Forum", 
                        Description = "Explore blockchain technology, cryptocurrencies, and decentralized finance applications.",
                        Location = "Tech Convention Center", 
                        EventDate = DateTime.Now.AddDays(120), 
                        Capacity = 200,
                        CategoryId = categories.FirstOrDefault(c => c.Name == "Technology")?.CategoryId,
                        CreatedAt = DateTime.Now
                    },
                    new Event 
                    { 
                        Title = "Leadership Development Program", 
                        Description = "Comprehensive leadership training for managers and aspiring leaders.",
                        Location = "Executive Training Center", 
                        EventDate = DateTime.Now.AddDays(40), 
                        Capacity = 80,
                        CategoryId = categories.FirstOrDefault(c => c.Name == "Business")?.CategoryId,
                        CreatedAt = DateTime.Now
                    }
                };

                await context.Events.AddRangeAsync(events);
            }
        }
    }
}
