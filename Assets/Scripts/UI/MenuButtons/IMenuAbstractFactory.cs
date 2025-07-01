public interface IMenuAbstractFactory
{ 
    IButtonFactory<T> GetFactory<T>();
}
