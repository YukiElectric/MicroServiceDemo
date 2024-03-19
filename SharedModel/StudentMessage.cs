namespace SharedModel
{
    public interface StudentMessage
    {
        int StudentID { get; set; }
        string StudentName { get; set; }

        string StudentClass { get; set; }

        string StudentAcademy { get; set; }
        double StudentCPA { get; set; }
    }
}
