using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
namespace tools
{
    public class Check : MonoBehaviour
    {
        public static bool IsAttackedGeneral(bool user, string name,KeyValuePair<int, int> Generral, StringBuilder[,] chessbord)
        {
       //     Check.chessbord = chessboard;
            ArrayList All_Attacked_Moves = new();
            for(int i = 1; i <= 10; i++)
            {
                for(int j = 1; j <= 9; j++)
                {
                    if(chessbord[i,j].Length != 0)
                    {
                        if (!chessbord[i,j].ToString().Substring(0, 1).Equals(name))
                        {
                            KeyValuePair<int, int> temp = new(i, j);
                            ArrayList test =
                                 chessbord[i, j].ToString().Substring(2, 2) switch
                                 {
                                     "ch" => ChessRules.Can_Go(temp, chessbord[i, j].ToString(), chessbord, !user),
                                     "ma" => ChessRules.Can_Go(temp, chessbord[i, j].ToString(), chessbord, !user),
                                     "ji" => ChessRules.Can_Go(temp, chessbord[i, j].ToString(), chessbord, !user),//ÐèÒªÐÞ¸Ä
                                     "pa" => ChessRules.Can_Go(temp, chessbord[i, j].ToString(), chessbord, !user),
                                     "bi" => ChessRules.Can_Go(temp, chessbord[i, j].ToString(), chessbord, !user),
                                     "zu" => ChessRules.Can_Go(temp, chessbord[i, j].ToString(), chessbord, !user),
                                     _ => new(),
                                 };
                            if (test.Count != 0)
                            {
                                foreach(KeyValuePair<int, int> v in test)
                                {
                                    All_Attacked_Moves.Add(v);
                                }
                                   
                            }
                            
                        }
                    }
                }
            }
            if (All_Attacked_Moves.Contains(Generral))
            {
                return true;
            }
            return false;
        }
        public static bool IsOver(bool user,string camp, KeyValuePair<int, int> Generral, StringBuilder[,] chessboard)
        {
            bool isover = true;
            for(int i = 1;i <= 10; i++)
            {
                for(int j = 1; j <= 9; j++)
                {
                    if(chessboard[i,j].Length != 0)
                    {
                        if (chessboard[i, j].ToString().Substring(0, 1).Equals(camp))
                        {
                            KeyValuePair<int, int> location = new(i, j);
                            ArrayList moves = ChessRules.Can_Go(location,chessboard[i,j].ToString(),chessboard,user);
                            if(moves.Count != 0)
                            {
                                foreach(KeyValuePair<int, int> v in moves)
                                {
                                    StringBuilder[,] boardbackup = new StringBuilder[11, 11];
                                    // boardbackup = chessboard;
                                    for (int m = 1; m <= 10; m++)
                                    {
                                        for (int n = 1; n <= 9; n++)
                                        {
                                            boardbackup[m, n] = new StringBuilder("");
                                            if (chessboard[m, n].Length != 0)
                                            {
                                                boardbackup[m, n].Append(chessboard[m, n]);

                                            }
                                        }
                                    }
                                    boardbackup[v.Key, v.Value].Clear();
                                    boardbackup[v.Key, v.Value].Append(boardbackup[location.Key,location.Value]);
                                    boardbackup[location.Key, location.Value].Clear();
                              //      Debug.Log(chessboard[1,1].ToString());
                                    if (boardbackup[v.Key, v.Value].ToString().Substring(2, 2).Equals("ji"))
                                    {
                                        if(!IsAttackedGeneral(user, camp, v, boardbackup))
                                        {
                          //                  Debug.Log(location + " " + v);
                                            isover = false;
                                            break;
                                        }
                                    }
                                    else if(!IsAttackedGeneral(user, camp, Generral, boardbackup))
                                    {
                             //           Debug.Log(boardbackup[v.Key, v.Value].ToString()+v);
                                        isover = false;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    if (!isover)
                    {
                        break;
                    }
                 //   Debug.Log(i + " " + j);
                }
                if (!isover)
                {
                    break;
                }
            }
            return isover;
        }
    }
}
