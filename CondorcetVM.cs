using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Condorcet
{
	class CondorcetVM : INotifyPropertyChanged
	{
		//View Model Private Variables
		int numVoter=0;
		int numCand=0;
		string pref;
		string res;

		//View Model Public Variables
		public String NumVoter
		{
			get { return numVoter.ToString(); }
			set { numVoter = int.Parse(value); OnPropertyChanged(); }
		}

		public String NumCand
		{
			get { return numCand.ToString(); }
			set { numCand = int.Parse(value); OnPropertyChanged(); }
		}

		public String Pref
		{
			get { return pref; }
			set { pref = value; OnPropertyChanged(); }
		}

		public String Res
		{
			get { return res; }
			set { res = value; OnPropertyChanged(); }
		}

		//View Model Global Variables to Store Ballot Count and Candidate Count
		int b, c;

		//Method to Parse Preferences String from View and Store it into Integer Array
		public void ParsePref(int[,] x)
		{
			int i = 0;
			int j = 0;

			string[] PrefStringArray=Pref.Split(new[] { Environment.NewLine}, StringSplitOptions.None);

			foreach(string str in PrefStringArray)
			{
				for(j = 0; j < numCand; j++)
				{
					x[i, j] = int.Parse(str[j].ToString());
				}

				++i;
			}
		}

		//Method to Find the Winner in Codorced Election
		public void FindWinner()
		{
			int winner = 0, wincount = 0, drawcount = 0;
			int i, j, k;
			int p, q;

			b = numVoter;
			c = numCand;

			//Used to Store Preference List of Voters
			int[,] prefList = new int[b, c];

			//Parsing Preferences from View and Storing it in 2 Dimensional Array prefList
			ParsePref(prefList);

			//Array of Preference Matrix for each Voter
			int[,,] prefmat = new int[b, c, c];
			
			//Aggregate Preference Matrix for all Voters
			int[,] summat = new int[c, c];

			//Matrix Storing Winner for each Candidate
			int[] winstat = new int[c];

			//Iterating to create Preference Matrix
			for (i = 0; i < b; i++)
				for (j = 0; j < c; j++)
					for (k = 0; k < c; k++)
					{
						p = prefList[i, j];
						q = prefList[i, k];
						if (k > j)
							prefmat[i, p, q] = 1;
						else
							prefmat[i, p, q] = 0;
					}

			//Iterating to create Aggregate Preference Matrix
			for (j = 0; j < c; j++)
				for (k = 0; k < c; k++)
					for (i = 0; i < b; i++)
					{
						summat[j, k] += prefmat[i, j, k];
					}

			//Iterating to find the Win Status matrix
			for (j = 0; j < c; j++)
				for (k = 0; k < c; k++)
				{
					winstat[j] += summat[j, k];
				}

			//Iterating to find the Winner
			for (j = 0; j < c; j++)
			{
				if (winstat[j] > wincount)
				{
					winner = j;
					wincount = winstat[j];
				}
			}

			//Iterating to find possibility of Match Draw
			for (j = 0; j < c; j++)
			{
				if (wincount == winstat[j])
					drawcount++;
			}

			//Returning Winner to View
			if (drawcount == 1)
				Res="Winner is :" + winner;

			else
				Res="No Condorcet Winner";
		}

		public void Reset()
		{
			numVoter = 0;
			NumVoter = "0";
			numCand = 0;
			NumCand = "0";
			pref = "";
			Pref = "";
			res = "";
			Res = "";
			b = 0;
			c = 0;
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged([CallerMemberName] String propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
