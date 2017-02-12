using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace net461.domain.nsubstitute.tests
{
    [TestClass]
    public class ActionManagerTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_ArgumentNullException()
        {
            // Act
            var actionManager = new ActionManager(null);
        }

        [TestMethod]
        public void Execute_ActionType_A()
        {
            var sub = Substitute.For<IAction>();

            var actionManager = new ActionManager(sub);

            actionManager.Execute(ActionTypes.A);

            sub.Received(1).Action1();
            sub.Received(1).Action2();
            sub.Received(1).Action3();
        }

        [TestMethod]
        public void Execute_ActionType_C_ThreeTimes()
        {
            var sub = Substitute.For<IAction>();

            sub.GetActionType().Returns(ActionTypes.C);

            var actionManager = new ActionManager(sub);

            actionManager.Execute();

            sub.Received(3).Action1();
            sub.Received(3).Action2();
            sub.Received(3).Action3();
        }

        [TestMethod]
        public void Execute_ActionType_Sequence()
        {
            var sub = Substitute.For<IAction>();

            sub.GetActionType().Returns(ActionTypes.C, ActionTypes.B, ActionTypes.A);
                        
            var actionManager = new ActionManager(sub);

            actionManager.Execute();

            Received.InOrder(() =>
            {
                sub.Received().Action3();
                sub.Received().Action1();
                sub.Received().Action2();
                sub.Received().Action3();
                sub.Received().Action2();
                sub.Received().Action1();
                sub.Received().Action1();
                sub.Received().Action2();
                sub.Received().Action3();
            });
        }
    }
}
