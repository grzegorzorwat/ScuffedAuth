namespace Authorization
{
    public interface ICodesGenerator<T>
    {
        T Generate();
    }
}
