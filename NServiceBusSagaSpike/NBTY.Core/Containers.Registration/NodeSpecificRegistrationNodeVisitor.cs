namespace NBTY.Core.Containers.Registration
{

    public class NodeSpecificRegistrationNodeVisitor<TNodeType> : IRegistrationNodeVisitor
    {
        IRegistrationNodeVisitor _actual_visitor;

        public NodeSpecificRegistrationNodeVisitor(IRegistrationNodeVisitor actualVisitor)
        {
            _actual_visitor = actualVisitor;
        }

        public void Process(IRegistrationNode item)
        {
            if (item.GetType() == typeof(TNodeType)) _actual_visitor.Process(item);
        }
    }
}