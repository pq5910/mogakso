using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_TextRPG2
{
    //게임 진행에 대한 전반적인 사항 관리하는 클래스
    public enum GameMode
    {
        None = 0,
        Lobby = 1,
        Town = 2,
        Field = 3
    }

    class Game
    {
        private GameMode _mode = GameMode.Lobby;
        private Player player = null;
        private Monster monster = null;
        Random rand = new Random();

        public void Process()
        {
            switch (_mode)
            {
                case GameMode.Lobby:
                    ProcessLobby();
                    break;
                case GameMode.Town:
                    ProcessTown();
                    break;
                case GameMode.Field:
                    ProcessFiled();
                    break;
            }
        }

        private void ProcessLobby()
        {
            Console.WriteLine("직업을 선택하세요");
            Console.WriteLine("[1] 기사");
            Console.WriteLine("[2] 궁수");
            Console.WriteLine("[3] 마법사");
            
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    player = new Knight();
                    _mode = GameMode.Town;
                    break;
                case "2":
                    player = new Archer();
                    _mode = GameMode.Town;
                    break;
                case "3":
                    player = new Mage();
                    _mode = GameMode.Town;
                    break;
            }
        }

        private void ProcessTown()
        {
            Console.WriteLine("마을에 입장했습니다!");
            Console.WriteLine("[1] 필드로 가기");
            Console.WriteLine("[2] 로비로 돌아가기");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    _mode = GameMode.Field;
                    break;
                case "2":
                    _mode = GameMode.Lobby;
                    break;
            }
        }

        private void ProcessFiled()
        {
            Console.WriteLine("필드에 입장했습니다!");
            Console.WriteLine("[1] 싸우기");
            Console.WriteLine("[2] 일정 확률로 마을에 돌아가기");

            CreateRandomMonster();

            string input = Console.ReadLine();
            switch(input)
            {
                case "1":
                    ProcessFight();
                    break;
                case "2":
                    TryEscape();
                    break;
            }
        }

        private void ProcessFight()
        {
            while (true)
            {
                int damage = player.GetAttack();
                monster.OnDamaged(damage);

                if (monster.IsDead())
                {
                    Console.WriteLine("승리했습니다!");
                    Console.WriteLine($"남은 체력 : {player.GetHP()}");
                    break;
                }

                damage = monster.GetAttack();
                player.OnDamaged(damage);

                if (player.IsDead())
                {
                    Console.WriteLine("패배했습니다!");
                    _mode = GameMode.Lobby;
                    break;
                }
            }
        }

        private void TryEscape()
        {
            int randValue = rand.Next(0, 101);
            if(randValue < 33)
            {
                Console.WriteLine("도망에 성공하였습니다.");
                _mode = GameMode.Town;
            }
            else
            {
                Console.WriteLine("도망에 실패하였습니다.");
                ProcessFight();
            }
        }

        private void CreateRandomMonster()
        {
            int randValue = rand.Next(0, 3);

            switch (randValue)
            {
                case 0:
                    monster = new Slime();
                    Console.WriteLine("슬라임이 나타났습니다!");
                    break;
                case 1:
                    monster = new Orc();
                    Console.WriteLine("오크가 나타났습니다!");
                    break;
                case 2:
                    monster = new Skeleton();
                    Console.WriteLine("스켈레톤이 나타났습니다!");
                    break;
            }
        }
    }
}
