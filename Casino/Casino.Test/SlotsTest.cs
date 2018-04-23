using System;
using Xunit;
using Casino.Library.Games;

namespace Casino.Test
{
    public class SlotsTest
    {
        Slots slots = new Slots();

        [Fact]
        public void T01Access()
        {
            Slots slots = new Slots();
            Assert.True(slots.status == "playing");
        }

        [Fact]
        public void T02SetLeft()
        {
            Slots slots = new Slots();
            slots.SetLeft();
            int x = slots.left;
            Assert.True(x == 7 || (x >= 1 && x <= 3));
        }

        [Fact]
        public void T03SetMiddle()
        {
            Slots slots = new Slots();
            slots.SetMiddle();
            int x = slots.middle;
            Assert.True(x == 7 || (x >= 1 && x <= 3));
        }

        [Fact]
        public void T04SetRight()
        {
            Slots slots = new Slots();
            slots.SetRight();
            int x = slots.right;
            Assert.True(x == 7 || (x >= 1 && x <= 3));
        }

        [Fact]
        public void T05CheckForMatchTrue()
        {
            Slots slots = new Slots();
            slots.left = 7;
            slots.middle = 7;
            slots.right = 7;
            Assert.True(slots.CheckForMatch() == "win");
        }

        [Fact]
        public void T06CheckForMatchFalse()
        {
            Slots slots = new Slots();
            slots.left = 1;
            slots.middle = 1;
            slots.right = 2;
            Assert.True(slots.CheckForMatch() == "lose");
            slots.left = 2;
            slots.middle = 1;
            slots.right = 1;
            Assert.True(slots.CheckForMatch() == "lose");
        }

        [Fact]
        public void T07GoldenTest()
        {
            Slots slots = new Slots();
            for(int i = 0; i <500; i++)
            {
                slots.SetLeft();
                Assert.True(slots.left == 7 || (slots.left >= 1 && slots.left <= 3));
                slots.SetMiddle();
                Assert.True(slots.middle == 7 || (slots.middle >= 1 && slots.middle <= 3));
                slots.SetRight();
                Assert.True(slots.right == 7 || (slots.right >= 1 && slots.right <= 3));
                slots.status = slots.CheckForMatch();
                Assert.True(slots.status == "win" || slots.status == "lose");
            }
        }


    }
}
