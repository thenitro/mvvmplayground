using MVVM.Models;
using NUnit.Framework;

namespace Tests.Editor.Models
{
    public class TestObservableValue
    {
        [TestCase(1)]
        [TestCase(-1)]
        public void ValueTypes_Work(int value)
        {
            var obs = new ObservableValue<int>();
            obs.Setup(value);
            Assert.False(obs.IsDefault());
            Assert.AreEqual(value, obs.Value);
        }
        
        [Test]
        public void IsDefault_IsTrue_ByDefault()
        {
            var obs = new ObservableValue<int>();
            Assert.True(obs.IsDefault());
        }
        
        [Test]
        public void IsDefault_IsTrue_IfSetNull()
        {
            var obs = new ObservableValue<TestClass>();
            Assert.True(obs.IsDefault());
            obs.Setup(null);
            Assert.True(obs.IsDefault());
            Assert.AreEqual(default, obs.Value);
        }

        private class TestClass { }
    }
}