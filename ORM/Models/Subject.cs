namespace ORM.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Credits { get; set; }
        public int TeacherId { get; set; }
        public Teacher? Teacher { get; set; }
        public int ClassroomId { get; set; }
        public Classroom? Classroom { get; set; }
    }
}