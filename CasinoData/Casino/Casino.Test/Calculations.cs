using System;
using Xunit;
using Casino.Library.Games;
using Casino.Library.Models;

namespace Casino.Test
{
    public class Calculations
    {

        [Fact]
        public void PlaceBetInGame()
        {
            User user = new User();
            ChipHelper ch = new ChipHelper();
            Chips chips = new Chips();

        

            user.UserPocket.AllChips[2].Amount = 50;
            ch.RemoveFromPocket(user.UserPocket, user.UserPocket.AllChips[2], 15*user.UserPocket.AllChips[2].Value);

            Assert.True(user.UserPocket.AllChips[2].Amount == 35);

            ch.AddToPocket(user.UserPocket, user.UserPocket.AllChips[2], 20*user.UserPocket.AllChips[2].Value);

            Assert.True(user.UserPocket.AllChips[2].Amount == 55);
        }

        [Fact]
        public void PocketSubtraction()
        {
            Pocket firstPocket = new Pocket();
            Pocket secondPocket = new Pocket();

            firstPocket.AllChips[0].Amount = 100;
            firstPocket.AllChips[1].Amount = 120;
            firstPocket.AllChips[2].Amount = 130;
            firstPocket.AllChips[3].Amount = 140;
            firstPocket.AllChips[4].Amount = 150;
            firstPocket.AllChips[5].Amount = 160;
            firstPocket.AllChips[6].Amount = 170;

             firstPocket.AllChips.Insert(0,new Chips(){Amount = 400, Type = "orange"});
             firstPocket.AllChips.Insert(1,new Chips(){Amount = 500, Type = "blue"});
             firstPocket.AllChips.Insert(2,new Chips(){Amount = 700, Type = "black"});
            
            secondPocket.AllChips[0].Amount = 1;
            secondPocket.AllChips[1].Amount = 2;
            secondPocket.AllChips[2].Amount = 3;
            secondPocket.AllChips[3].Amount = 4;
            secondPocket.AllChips[4].Amount = 5;
            secondPocket.AllChips[5].Amount = 6;
            secondPocket.AllChips[6].Amount = 7;

             secondPocket.AllChips.Insert(0,new Chips(){Amount = 700, Type = "white"});
             secondPocket.AllChips.Insert(1,new Chips(){Amount = 300, Type = "red"});
             secondPocket.AllChips.Insert(2,new Chips(){Amount = 450, Type = "purple"});

            (new ChipHelper()).pocketSubtraction(firstPocket, secondPocket);

            Assert.True(firstPocket.AllChips[0].Amount == 99);
            Assert.True(firstPocket.AllChips[1].Amount == 118);
            Assert.True(firstPocket.AllChips[2].Amount == 127);
            Assert.True(firstPocket.AllChips[3].Amount == 136);
            Assert.True(firstPocket.AllChips[4].Amount == 145);
            Assert.True(firstPocket.AllChips[5].Amount == 154);
            Assert.True(firstPocket.AllChips[6].Amount == 163);

            Assert.True(secondPocket.AllChips[0].Amount == 1);
            Assert.True(secondPocket.AllChips[1].Amount == 2);
            Assert.True(secondPocket.AllChips[2].Amount == 3);
            Assert.True(secondPocket.AllChips[3].Amount == 4);
            Assert.True(secondPocket.AllChips[4].Amount == 5);
            Assert.True(secondPocket.AllChips[5].Amount == 6);
            Assert.True(secondPocket.AllChips[6].Amount == 7);

            Assert.True(firstPocket.AllChips.Count == 7);
            Assert.True(secondPocket.AllChips.Count == 7);

            Assert.True(firstPocket.AllChips[0].Type.Equals("orange"));
            Assert.True(firstPocket.AllChips[1].Type.Equals("purple"));
            Assert.True(firstPocket.AllChips[2].Type.Equals("black"));
            Assert.True(firstPocket.AllChips[3].Type.Equals("green"));
            Assert.True(firstPocket.AllChips[4].Type.Equals("blue"));
            Assert.True(firstPocket.AllChips[5].Type.Equals("red"));
            Assert.True(firstPocket.AllChips[6].Type.Equals("white"));


            Assert.True(secondPocket.AllChips[0].Type.Equals("orange"));
            Assert.True(secondPocket.AllChips[1].Type.Equals("purple"));
            Assert.True(secondPocket.AllChips[2].Type.Equals("black"));
            Assert.True(secondPocket.AllChips[3].Type.Equals("green"));
            Assert.True(secondPocket.AllChips[4].Type.Equals("blue"));
            Assert.True(secondPocket.AllChips[5].Type.Equals("red"));
            Assert.True(secondPocket.AllChips[6].Type.Equals("white"));

            
        }
    }
}
