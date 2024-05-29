//using Microsoft.EntityFrameworkCore.Design;
//using Microsoft.EntityFrameworkCore;

//namespace SigmaApp.DataContext;

//public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
//{
//    public ApplicationDbContext CreateDbContext(string[] args)
//    {
//        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
//        optionsBuilder.UseInMemoryDatabase("CandidateDB");

//        return new ApplicationDbContext(optionsBuilder.Options);
//    }
//}