using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveData {

    //Max number of entires in leaderboard
    const int maxEntires = 5;
    //location of txt file
    static private string path = Application.persistentDataPath + "/data.txt";


   static public void UpdateScore(int score)
    {
        //Gets existing entries
        List<LeaderBoardEntry> previousScore = GetCurrentHighScore();

        FileStream stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);

        //The newest score added
        previousScore.Add(new LeaderBoardEntry("", score));

        //Returns 1 if a.score is bigger, -1 if not
        previousScore.Sort((a, b) => { return (a.score > b.score ? 1 : -1); });

        //Checks the number of entries
        if(previousScore.Count > maxEntires)
        {
            //Removes last entry from list if there are too many
            previousScore.RemoveAt(previousScore.Count - 1);
        }

        StreamWriter writer = new StreamWriter(stream);

        //for how many entries there are
        for (int i = 0; i < previousScore.Count; ++i)
        {
            //Write the name and score
            if(previousScore[i].name == "")
            {
                previousScore[i].name = "NewPlayer";
            }
            writer.WriteLine(previousScore[i].name + " " + previousScore[i].score);
          
        }

        //Flush it from the buffer
        writer.Flush();
        writer.Close();
        writer.Dispose();


    }

    public static LeaderBoard GetLeaderBoard()
    {
        return new LeaderBoard();
    }

    public static List<LeaderBoardEntry> GetCurrentHighScore()
    {
        
        FileStream stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);


        List<LeaderBoardEntry> scores = new List<LeaderBoardEntry>();

        //If file exists
        if (System.IO.File.Exists(path))
        {
            StreamReader reader = new StreamReader(stream);

            while(!reader.EndOfStream)
            {   //Reads each line
                string data = reader.ReadLine();

                //data split by a space
                string[] elements = data.Split(' ');

                string name = elements[0];
                int score = int.Parse(elements[1]);

                //Adds each entry to the list
                scores.Add(new LeaderBoardEntry(name, score));
            }
            reader.Close();
        }

        return scores;
    }

        //score = Movement.points;
        //print(score);
    
	

}

public class LeaderBoard
{

    List<LeaderBoardEntry> entries;

    public LeaderBoard()
    {
        //fill list with existing lines
        entries = SaveData.GetCurrentHighScore();
    }

    public override string ToString()
    {
        List<LeaderBoardEntry> scores = SaveData.GetCurrentHighScore();

        string str = string.Empty;

        for (int i = 0; i < scores.Count; ++i)
        {
            //Turns leaderboard data into a string
            str += scores[i].ToString() + (i < scores.Count - 1 ? "\n" : "");
        }

        return str;
    }
}

public class LeaderBoardEntry
{

    public string name;
    public int score;

    public LeaderBoardEntry(string name, int score)
    {
        //name and score defined in the class
        this.name = name;
        this.score = score;
    }

    public override string ToString()
    {
        //Turn into string
        return name + ": " + score;
    }
}