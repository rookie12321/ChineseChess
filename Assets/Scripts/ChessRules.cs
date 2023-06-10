using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace tools {
    public class ChessRules : MonoBehaviour
    {
        private static bool user;//true为玩家，false为对手
        private static string camp;
        private static KeyValuePair<int, int> location;
        private static StringBuilder[,] chessboard;
        public static ArrayList Can_Go(KeyValuePair<int, int> location, string name, StringBuilder[,] chessboard,bool user)
        {
            ChessRules.user = user;
            ChessRules.chessboard = chessboard;
      //      Debug.Log("name : "+ ChessRules.chessboard[location.Key, location.Value].ToString());
            ChessRules.location = location;
            camp = name.Substring(0,1);
            return name.Substring(2, 2) switch
            {
                "ch" => Che(),
                "ma" => Ma(),
                "xi" => Xiang(),
                "sh" => Shi(),
                "ji" => Jiang(),
                "pa" => Pao(),
                "bi" => Bing_Zu(),
                "zu" => Bing_Zu(),
                _ => new(),
            };
        }
        private static bool jud_overstep(int x,int y)//判断是否越界
        {
            if (x > 0 && x < 11 && y > 0 && y < 10)
            {
                return true;
            }
            return false;
        }
        private static ArrayList Jiang()
        {
            ArrayList all_moves = new();
            int[,] step = new int[4, 2] { { -1, 0 }, { 1, 0 }, { 0, 1 }, { 0, -1 } };
            if (user)
            {
                for (int i = 0; i < 4; i++)
                {
                    int new_x = location.Key + step[i, 0], new_y = location.Value + step[i, 1];
                    if (new_x < 4 && new_y >= 4 && new_y <= 6 && new_x >= 1)
                    {
                        if (chessboard[new_x, new_y].Length == 0)
                        {
                            KeyValuePair<int, int> temp = new(location.Key + step[i, 0], location.Value + step[i, 1]);
                            all_moves.Add(temp);
                        }
                        else if (!chessboard[new_x, new_y].ToString().Substring(0, 1).Equals(camp))
                        {
                            KeyValuePair<int, int> temp = new(location.Key + step[i, 0], location.Value + step[i, 1]);
                            all_moves.Add(temp);
                        }
                    }
                }
                for(int i = 1; i <= 9; i++)//对将
                {
                    if (jud_overstep(location.Key + i, location.Value))
                    {
                        if(chessboard[location.Key + i, location.Value].Length == 0)
                        {

                        }
                        else if(chessboard[location.Key+i,location.Value].ToString().Substring(2,2).Equals("ji"))
                        {
                            KeyValuePair<int, int> temp = new(location.Key + i, location.Value);
                            all_moves.Add(temp);
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    int new_x = location.Key + step[i, 0], new_y = location.Value + step[i, 1];
                    if (new_x <= 10 && new_y >= 4 && new_y <= 6 && new_x >= 8)
                    {
                        if (chessboard[new_x, new_y].Length == 0)
                        {
                            KeyValuePair<int, int> temp = new(location.Key + step[i, 0], location.Value + step[i, 1]);
                            all_moves.Add(temp);
                        }
                        else if (!chessboard[new_x, new_y].ToString().Substring(0, 1).Equals(camp))
                        {
                            KeyValuePair<int, int> temp = new(location.Key + step[i, 0], location.Value + step[i, 1]);
                            all_moves.Add(temp);
                        }
                    }
                }
                for (int i = 1; i <= 9; i++)//对将
                {
                    if (jud_overstep(location.Key - i, location.Value))
                    {
                        if (chessboard[location.Key - i, location.Value].Length == 0)
                        {

                        }
                        else if (chessboard[location.Key - i, location.Value].ToString().Substring(2, 2).Equals("ji"))
                        {
                            KeyValuePair<int, int> temp = new(location.Key - i, location.Value);
                            all_moves.Add(temp);
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return all_moves;
        }
        private static ArrayList Shi()
        {
            ArrayList all_moves = new();
            int[,] step = new int[4, 2] { { -1, -1 }, { 1, -1 }, { -1, 1 }, {1, 1 } };
            if (user)
            {
                for (int i = 0; i < 4; i++)
                {
                    int new_x = location.Key + step[i, 0], new_y = location.Value + step[i, 1];
                    if (new_x < 4 && new_y >= 4 && new_y <= 6 && new_x >= 1)
                    {
                        if (chessboard[new_x, new_y].Length == 0)
                        {
                            KeyValuePair<int, int> temp = new(location.Key + step[i, 0], location.Value + step[i, 1]);
                            all_moves.Add(temp);
                        }
                        else if (!chessboard[new_x, new_y].ToString().Substring(0, 1).Equals(camp))
                        {
                            KeyValuePair<int, int> temp = new(location.Key + step[i, 0], location.Value + step[i, 1]);
                            all_moves.Add(temp);
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    int new_x = location.Key + step[i, 0], new_y = location.Value + step[i, 1];
                    if (new_x <= 10 && new_y >= 4 && new_y <= 6 && new_x >= 8)
                    {
                        if (chessboard[new_x, new_y].Length == 0)
                        {
                            KeyValuePair<int, int> temp = new(location.Key + step[i, 0], location.Value + step[i, 1]);
                            all_moves.Add(temp);
                        }
                        else if (!chessboard[new_x, new_y].ToString().Substring(0, 1).Equals(camp))
                        {
                            KeyValuePair<int, int> temp = new(location.Key + step[i, 0], location.Value + step[i, 1]);
                            all_moves.Add(temp);
                        }
                    }
                }
            }
            return all_moves;
        }
        private static ArrayList Bing_Zu()
        {
            ArrayList all_moves = new();
            int[,] enemy_step = new int[3, 2]{ { -1, 0 }, { 0, -1 }, { 0, 1 } };
            int[,] player_step = new int[3, 2]{ { 1, 0 }, { 0, -1 }, { 0, 1 } };
            if (user)
            {
                if (jud_overstep(location.Key + player_step[0, 0], location.Value + player_step[0, 1]))
                {
                    if(chessboard[location.Key + player_step[0, 0], location.Value + player_step[0, 1]].Length == 0)
                    {
                        KeyValuePair<int, int> temp = new(location.Key + player_step[0, 0], location.Value + player_step[0, 1]);
                        all_moves.Add(temp);
                    }
                    else if(!chessboard[location.Key + player_step[0, 0], location.Value + player_step[0, 1]].ToString().Substring(0, 1).Equals(camp))
                    {
                        KeyValuePair<int, int> temp = new(location.Key + player_step[0, 0], location.Value + player_step[0, 1]);
                        all_moves.Add(temp);
                    }
                }
                if (location.Key > 5)
                {
                    for (int i = 1; i <= 2; i++)
                    {
                        if (jud_overstep(location.Key + enemy_step[i, 0], location.Value + enemy_step[i, 1]))
                        {
                            if (chessboard[location.Key + enemy_step[i, 0], location.Value + enemy_step[i, 1]].Length == 0)
                            {
                                KeyValuePair<int, int> temp = new(location.Key + enemy_step[i, 0], location.Value + enemy_step[i, 1]);
                                all_moves.Add(temp);
                            }
                            else if (!chessboard[location.Key + enemy_step[i, 0], location.Value + enemy_step[i, 1]].ToString().Substring(0, 1).Equals(camp))
                            {
                                KeyValuePair<int, int> temp = new(location.Key + enemy_step[i, 0], location.Value + enemy_step[i, 1]);
                                all_moves.Add(temp);
                            }
                        }
                    }
                }
            }
            else
            {
                if (jud_overstep(location.Key + enemy_step[0, 0], location.Value + enemy_step[0, 1]))
                {
                    if (chessboard[location.Key + enemy_step[0, 0], location.Value + enemy_step[0, 1]].Length == 0)
                    {
                        KeyValuePair<int, int> temp = new(location.Key + enemy_step[0, 0], location.Value + enemy_step[0, 1]);
                        all_moves.Add(temp);
                    }
                    else if (!chessboard[location.Key + enemy_step[0, 0], location.Value + enemy_step[0, 1]].ToString().Substring(0, 1).Equals(camp))
                    {
                        KeyValuePair<int, int> temp = new(location.Key + enemy_step[0, 0], location.Value + enemy_step[0, 1]);
                        all_moves.Add(temp);
                    }
                }
                if (location.Key < 6)
                {
                    for (int i = 1; i <= 2; i++)
                    {
                        if (jud_overstep(location.Key + enemy_step[i, 0], location.Value + enemy_step[i, 1]))
                        {
                            if (chessboard[location.Key + enemy_step[i, 0], location.Value + enemy_step[i, 1]].Length == 0)
                            {
                                KeyValuePair<int, int> temp = new(location.Key + enemy_step[i, 0], location.Value + enemy_step[i, 1]);
                                all_moves.Add(temp);
                            }
                            else if (!chessboard[location.Key + enemy_step[i, 0], location.Value + enemy_step[i, 1]].ToString().Substring(0, 1).Equals(camp))
                            {
                                KeyValuePair<int, int> temp = new(location.Key + enemy_step[i, 0], location.Value + enemy_step[i, 1]);
                                all_moves.Add(temp);
                            }
                        }
                    }
                }
            }
            return all_moves;
        }
        private static ArrayList Ma()
        {
            ArrayList all_moves = new();
            int[,] step = new int[8,2] {{ 1, -2 }, { 2, -1 }, { 2, 1 }, { 1, 2 }, { -1, 2 }, { -2, 1 }, { -2, -1 }, { -1, -2 } };
            int[,] obstacle = new int[8, 2] { { 0, -1 }, { 1, 0 }, { 1, 0 }, { 0, 1 }, { 0, 1 }, { -1, 0 }, { -1, 0 }, { 0, -1 } };
            for(int i = 0; i < 8; i++)
            {
                if (jud_overstep(location.Key + step[i, 0], location.Value + step[i, 1]))
                {
                    if(chessboard[location.Key + obstacle[i, 0], location.Value + obstacle[i, 1]].Length==0)

                    {
                        if(chessboard[location.Key + step[i, 0], location.Value + step[i, 1]].Length != 0)
                        {
                            if(!chessboard[location.Key + step[i, 0], location.Value + step[i, 1]].ToString().Substring(0, 1).Equals(camp))
                            {
                                KeyValuePair<int, int> temp = new(location.Key + step[i, 0], location.Value + step[i, 1]);
                                all_moves.Add(temp);
                            }
                        }
                        else
                        {
                            KeyValuePair<int, int> temp = new(location.Key + step[i, 0], location.Value + step[i, 1]);
                            all_moves.Add(temp);
                        }
                        
                    }
                }
            }
            return all_moves;
        }
        private static ArrayList Pao()
        {
            ArrayList all_moves = new();
            int[] step = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            for(int i = 0,cnt = 0; i <= 7; i++)
            {
                if (jud_overstep(location.Key, location.Value + step[i]))
                {

                    if (cnt == 0 && chessboard[location.Key, location.Value + step[i]].Length == 0)
                    {
                        KeyValuePair<int, int> temp = new(location.Key, location.Value + step[i]);
                        all_moves.Add(temp);
                    }
                    else if(chessboard[location.Key, location.Value + step[i]].Length != 0)
                    {
                        if (cnt == 1 && !chessboard[location.Key, location.Value + step[i]].ToString().Substring(0, 1).Equals(camp))
                        {
                            KeyValuePair<int, int> temp = new(location.Key, location.Value + step[i]);
                            all_moves.Add(temp);
                        }
                        cnt++;
                    }
                }
            }            
            for(int i = 0,cnt = 0; i <= 7; i++)
            {
                if (jud_overstep(location.Key, location.Value - step[i]))
                {

                    if (cnt == 0 && chessboard[location.Key, location.Value - step[i]].Length == 0)
                    {
                        KeyValuePair<int, int> temp = new(location.Key, location.Value - step[i]);
                        all_moves.Add(temp);
                    }
                    else if(chessboard[location.Key, location.Value - step[i]].Length != 0)
                    {
                        if (cnt == 1 && !chessboard[location.Key, location.Value - step[i]].ToString().Substring(0, 1).Equals(camp))
                        {
                            KeyValuePair<int, int> temp = new(location.Key, location.Value - step[i]);
                            all_moves.Add(temp);
                        }
                        cnt++;
                    }
                }
            }
            for (int i = 0, cnt = 0; i <= 8; i++)
            {
                if (jud_overstep(location.Key + step[i], location.Value))
                {
                    
                    if (cnt == 0 && chessboard[location.Key + step[i], location.Value].Length == 0)
                    {
                        KeyValuePair<int, int> temp = new(location.Key + step[i], location.Value);
                        all_moves.Add(temp);
                    }
                    else if (chessboard[location.Key + step[i], location.Value].Length != 0)
                    {
                        
                        if (cnt == 1 && !chessboard[location.Key + step[i], location.Value].ToString().Substring(0, 1).Equals(camp))
                        {
                            KeyValuePair<int, int> temp = new(location.Key + step[i], location.Value);
                            all_moves.Add(temp);
                            //Debug.Log("cnt: " + cnt + " " + temp);
                        }
                        cnt++;
                    }
                    
                }
            }            
            for (int i = 0, cnt = 0; i <= 8; i++)
            {
                if (jud_overstep(location.Key - step[i], location.Value))
                {

                    if (cnt == 0 && chessboard[location.Key - step[i], location.Value].Length == 0)
                    {
                        KeyValuePair<int, int> temp = new(location.Key - step[i], location.Value);
                        all_moves.Add(temp);
                    }
                    else if (chessboard[location.Key - step[i], location.Value].Length != 0)
                    {
                        if (cnt == 1 && !chessboard[location.Key - step[i], location.Value].ToString().Substring(0, 1).Equals(camp))
                        {
                            KeyValuePair<int, int> temp = new(location.Key - step[i], location.Value);
                            all_moves.Add(temp);
                        }
                        cnt++;
                    }
                }
            }
            return all_moves;
        }
        private static ArrayList Xiang()
        {
            ArrayList all_moves = new();
            int[,] step = new int[4,2]{ { -2, -2}, { 2, -2}, { -2, 2}, { 2, 2} };
            int[,] obstacle = new int[4, 2] { { -1, -1 }, { 1, -1 }, { -1, 1 }, { 1, 1 } };
            bool small = location.Key < 6 ? true : false;
            for (int i = 0; i < 4; i++)
            {
                if (jud_overstep(location.Key + step[i, 0], location.Value + step[i, 1])&&((location.Key + step[i, 0]<6&&small)|| (location.Key + step[i, 0]>5&&!small)))//
                {
                    if (chessboard[location.Key + obstacle[i, 0], location.Value + obstacle[i, 1]].Length == 0)//判断象腿有无棋子
                    {
                        if (chessboard[location.Key + step[i, 0], location.Value + step[i, 1]].Length != 0)
                        {
                            if (!chessboard[location.Key + step[i, 0], location.Value + step[i, 1]].ToString().Substring(0, 1).Equals(camp))
                            {
                                KeyValuePair<int, int> temp = new(location.Key + step[i, 0], location.Value + step[i, 1]);
                                all_moves.Add(temp);
                            }
                        }
                        else
                        {
                            KeyValuePair<int, int> temp = new(location.Key + step[i, 0], location.Value + step[i, 1]);
                            all_moves.Add(temp);
                        }

                    }
                }
            }
            return all_moves;
        }
        private static ArrayList Che()
        {
            //Debug.Log("chess : "+chessboard[location.Key,location.Value].ToString());
            ArrayList all_moves = new();
            int[] step = { 1, 2, 3, 4, 5, 6, 7, 8 ,9};
            bool across_add = true;
            bool across_minus = true;
            for (int i = 0; i <= 7; i++)
            {
                //Debug.Log("this : "+location.Value + step[i]);
                if (across_add && (location.Value + step[i]) < 10 )
                {
                    
                    if (chessboard[location.Key, location.Value + step[i]].Length == 0)
                    {
                        KeyValuePair<int, int> temp = new(location.Key, location.Value + step[i]);
                        all_moves.Add(temp);
                    }
                    else if (!chessboard[location.Key, location.Value + step[i]].ToString().Substring(0, 1).Equals(camp))
                    {
                        //Debug.Log(chessboard[location.Key, location.Value + step[i]].ToString().Substring(0, 1) + " " + camp);
                        KeyValuePair<int, int> temp = new(location.Key, location.Value + step[i]);
                        all_moves.Add(temp);
                        across_add = false;
                    }
                    else
                    {
                        //Debug.Log(chessboard[location.Key, location.Value + step[i]].ToString().Substring(0, 1) + " " + camp);
                        across_add = false;
                    }
                    
                }
                else
                {
                    across_add = false;
                }
                if (across_minus && (location.Value - step[i]) > 0)
                {
                    if(chessboard[location.Key, location.Value - step[i]].Length == 0)
                    {
                        KeyValuePair<int, int> temp = new(location.Key, location.Value - step[i]);
                        all_moves.Add(temp);
                    }
                    else if(!chessboard[location.Key, location.Value - step[i]].ToString().Substring(0, 1).Equals(camp))
                    {
                        KeyValuePair<int, int> temp = new(location.Key, location.Value - step[i]);
                        all_moves.Add(temp);
                        across_minus = false;
                    }
                    else
                    {
                        across_minus = false;
                    }
                }
                else
                {
                    across_minus = false;
                }
                if (!across_add && !across_minus)
                {
                    break;
                }
            }
            across_add = true;
            across_minus = true;
            for (int i = 0; i <= 8; i++)
            {
                if (across_add && (location.Key + step[i]) < 11)
                {
                    if(chessboard[location.Key + step[i], location.Value].Length == 0)
                    {
                        KeyValuePair<int, int> temp = new(location.Key + step[i], location.Value);
                        all_moves.Add(temp);
                    }
                    else if(!chessboard[location.Key + step[i], location.Value].ToString().Substring(0, 1).Equals(camp))
                    {
                        KeyValuePair<int, int> temp = new(location.Key + step[i], location.Value);
                        all_moves.Add(temp);
                        across_add = false;
                    }
                    else
                    {
                        across_add = false;
                    }
                }
                else
                {
                    across_add = false;
                }
                if (across_minus && (location.Key - step[i]) > 0)
                {
                    if(chessboard[location.Key - step[i], location.Value].Length == 0)
                    {
                        KeyValuePair<int, int> temp = new(location.Key - step[i], location.Value);
                        all_moves.Add(temp);
                    }
                    else if(!chessboard[location.Key - step[i], location.Value].ToString().Substring(0, 1).Equals(camp))
                    {
                        KeyValuePair<int, int> temp = new(location.Key - step[i], location.Value);
                        all_moves.Add(temp);
                        across_minus = false;
                    }
                    else
                    {
                        across_minus = false;
                    }
                }
                else
                {
                    across_minus = false;
                }
                if (!across_add && !across_minus)
                {
                    break;
                }
            }
            return all_moves;
        }
    }
}
