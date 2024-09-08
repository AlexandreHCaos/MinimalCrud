using MinimalCrud.Data;
namespace MinimalCrud.Students;
public static class StudentsRoutes
{
    public static void AddStudentsRoutes(this WebApplication app)
    {
        var studentsRoutes = app.MapGroup("Students");

        studentsRoutes.MapPost("", async (AddStudentRequest request, AppDbContext context) => {
            var student = new Student(request.Name);

            await context.Students.AddAsync(student);
            await context.SaveChangesAsync();
        });
    }
}