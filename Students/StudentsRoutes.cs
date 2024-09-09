using Microsoft.EntityFrameworkCore;
using MinimalCrud.Data;
namespace MinimalCrud.Students;
public static class StudentsRoutes
{
    public static void AddStudentsRoutes(this WebApplication app)
    {
        var studentsRoutes = app.MapGroup("Students");

        studentsRoutes.MapPost("", async (AddStudentRequest request, AppDbContext context, CancellationToken ct) => {
            var exists = await context.Students.AnyAsync(x => x.Name == request.Name, ct);

            if (exists)
                return Results.Conflict("Student already exists.");

            var student = new Student(request.Name);
            await context.Students.AddAsync(student, ct);
            await context.SaveChangesAsync(ct);

            var studentDto = new StudentDto(student.Id, student.Name);

            return Results.Ok(studentDto);
        });

        studentsRoutes.MapGet("", async (AppDbContext context, CancellationToken ct) => {
            var students = await context.Students.Where(x => x.isActive).Select(x => new StudentDto(x.Id, x.Name)).ToListAsync(ct);

            return Results.Ok(students);
        });

        studentsRoutes.MapPut("{id:guid}", async(Guid id, UpdateStudentRequest request, AppDbContext context, CancellationToken ct) => {
            var student = await context.Students.SingleOrDefaultAsync(x => x.Id == id, ct);

            if (student == null)
                return Results.NotFound();

            student.UpdateName(request.Name);
            await context.SaveChangesAsync(ct);
            return Results.Ok(new StudentDto(student.Id, student.Name));
        });

        studentsRoutes.MapDelete("{id:guid}", async (Guid id, AppDbContext context, CancellationToken ct) => {
            var student = await context.Students.SingleOrDefaultAsync(x => x.Id == id, ct);
            
            if (student == null)
                return Results.NotFound();
            
            student.Inactivate();
            await context.SaveChangesAsync(ct);
            return Results.Ok();
        });
    }
}