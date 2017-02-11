namespace net461.domain.moq.tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using net461.domain;
    using System.Collections;
    using System.Collections.Generic;

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
            var mock = new Mock<IAction>();

            var actionManager = new ActionManager(mock.Object);

            actionManager.Execute(ActionTypes.A);

            mock.Verify(m => m.Action1(), Times.Once);
            mock.Verify(m => m.Action2(), Times.Once);
            mock.Verify(m => m.Action3(), Times.Once);
        }

        [TestMethod]
        public void Execute_ActionType_C_ThreeTimes()
        {
            var iaction = Mock.Of<IAction>(m => m.GetActionType() == ActionTypes.C);

            var actionManager = new ActionManager(iaction);

            actionManager.Execute();

            Mock.Get(iaction).Verify(m => m.Action3(), Times.Exactly(3));
            Mock.Get(iaction).Verify(m => m.Action1(), Times.Exactly(3));
            Mock.Get(iaction).Verify(m => m.Action2(), Times.Exactly(3));
        }

        [TestMethod]
        public void Execute_ActionType_Sequence()
        {
            var mock = new Mock<IAction>(MockBehavior.Strict);
            
            mock.Setup(m => m.GetActionType())
                .Returns(new Queue<ActionTypes>(new[] { ActionTypes.C, ActionTypes.B, ActionTypes.A }).Dequeue);

            var mockSequence = new MockSequence();

            mock.InSequence(mockSequence).Setup(m => m.Action3());
            mock.InSequence(mockSequence).Setup(m => m.Action1());
            mock.InSequence(mockSequence).Setup(m => m.Action2());

            mock.InSequence(mockSequence).Setup(m => m.Action3());
            mock.InSequence(mockSequence).Setup(m => m.Action2());
            mock.InSequence(mockSequence).Setup(m => m.Action1());

            mock.InSequence(mockSequence).Setup(m => m.Action1());
            mock.InSequence(mockSequence).Setup(m => m.Action2());
            mock.InSequence(mockSequence).Setup(m => m.Action3());

            var actionManager = new ActionManager(mock.Object);

            actionManager.Execute();
        }
    }
}
