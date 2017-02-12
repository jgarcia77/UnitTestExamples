namespace netcore.domain
{
    public enum ActionTypes { A, B, C }

    public interface IAction
    {
        ActionTypes GetActionType();
        void Action1();
        void Action2();
        void Action3();
    }
}
