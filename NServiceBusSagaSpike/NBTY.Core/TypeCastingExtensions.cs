namespace NBTY.Core
{
    public static class TypeCastingExtensions
    {
        public static TDesiredType CastTo<TDesiredType>(this object item)
        {
            return (TDesiredType) item;
        } 
    }
}