public interface IButtonAbstractFactory
{ 
    IButtonFactory<T> GetFactory<T>();
}
