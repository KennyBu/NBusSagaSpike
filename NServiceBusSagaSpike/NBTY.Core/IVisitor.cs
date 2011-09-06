namespace NBTY.Core
{
    public interface IVisitor<TItemToVisit>
    {
        void Process(TItemToVisit item);
    }
}