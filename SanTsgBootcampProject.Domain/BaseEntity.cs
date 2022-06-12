namespace SanTsgBootcampProject.Domain
{
    /// <summary>
    ///This class takes generic classes. it avoids making code repetitions and makes code more maintainable
    /// </summary>
    /// <typeparam name="T"> Api response classes</typeparam>
    public class BaseEntity<T> where T : class
    {
        public T Body { get; set; }

    }
}
