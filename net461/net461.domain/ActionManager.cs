namespace net461.domain
{
    using System;

    public class ActionManager
    {
        private readonly IAction Action;

        public ActionManager(IAction action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("IAction must be injected.");
            }

            this.Action = action;
        }

        public void Execute()
        {
            var actionType = this.Action.GetActionType();

            this.Execute(actionType);

            actionType = this.Action.GetActionType();

            this.Execute(actionType);

            actionType = this.Action.GetActionType();

            this.Execute(actionType);
        }

        public void Execute(ActionTypes actionType)
        {
            switch (actionType)
            {
                case ActionTypes.A:
                    this.Action.Action1();
                    this.Action.Action2();
                    this.Action.Action3();
                    break;

                case ActionTypes.B:
                    this.Action.Action3();
                    this.Action.Action2();
                    this.Action.Action1();
                    break;

                case ActionTypes.C:
                    this.Action.Action3();
                    this.Action.Action1();
                    this.Action.Action2();
                    break;
            }
        }
    }
}
