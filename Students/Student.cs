namespace MinimalCrud.Students;

public class Student
{
    public Guid Id { get; init; }
    public string Name { get; private set;  }
    public bool isActive { get; private set; }

    public Student(string name)
    {
        Name = name;
        Id = Guid.NewGuid();
        isActive = true;
    }
}