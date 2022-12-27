using System.Text;

namespace Advent_of_Code.Days;

public class Day8 : IDay
{
    public void Setup(HttpClient client)
    {
        String input = ExtraFunctions.MakeAdventOfCodeInputRequest(client, 8);
        //input = "30373\n25512\n65332\n33549\n35390";

        foreach (String s in input.Split('\n', StringSplitOptions.RemoveEmptyEntries))
        {
            List<Tree> treeRow = new List<Tree>();
            foreach (Char c in s)
            {
                treeRow.Add(new Tree(Int32.Parse(c.ToString())));
            }

            Trees.Add(treeRow);
        }

        Int32 number = 58 * 85;
        StringBuilder sb = new StringBuilder(Trees[0].Count);
        for (int index = 0; index < Trees.Count; index++)
        {
            List<Tree> trees = Trees[index];
            for (int i = 0; i < trees.Count; i++)
            {
                Tree tree = trees[i];
                if (number == index * i)
                {
                    sb.Append("|" + tree.TreeHeight + "|");
                }
                else
                {
                    sb.Append(tree.TreeHeight);
                }
            }

            Console.WriteLine(sb.ToString());
            sb.Clear();
        }
    }

    List<List<Tree>> Trees = new List<List<Tree>>();

    public class Tree
    {
        /// <summary>
        /// North, East, South, West
        /// </summary>
        public Int32[] ScenicScoreArray = Enumerable.Repeat(0, 4).ToArray();

        public Int32 ScenicScore => ScenicScoreArray.Multiply();
        public Boolean Visible = false;
        public Boolean TallestFromLeft = false;
        public Boolean TallestFromRight = false;
        public Boolean TallestFromTop = false;
        public Boolean TallestFromBottom = false;

        public Int32 TreeHeight;

        public Tree(Int32 treeHeight)
        {
            TreeHeight = treeHeight;
        }

        public Tree(Int32 treeHeight, Boolean tallestFromLeft, Boolean visibleRight, Boolean visibleTop,
            Boolean visibleBottom)
        {
            TallestFromLeft = tallestFromLeft;
            TallestFromRight = visibleRight;
            TallestFromTop = visibleTop;
            TallestFromBottom = visibleBottom;
            TreeHeight = treeHeight;
        }
    }

    public void Challenge1()
    {
        FindAllTreesVisible();

        Int32 count = CalculateTotalVisibleTrees(Trees);
    }

    public void Challenge2()
    {
        Int32 score = FindHighestScenicTreeScore();
    }

    public void FindAllTreesVisible()
    {
        // Left to right
        foreach (List<Tree> trees in Trees)
        {
            Int32 leftTreeIndex = 0;
            Int32 rightTreeIndex = trees.Count - 1;

            Int32 leftTallestTree = 0;
            Int32 rightTallestTree = 0;

            Tree leftTree = trees[leftTreeIndex];
            Tree rightTree = trees[rightTreeIndex];

            leftTree.Visible = true;
            rightTree.Visible = true;

            while (rightTreeIndex is not 0)
            {
                leftTree = trees[leftTreeIndex];
                rightTree = trees[rightTreeIndex];
                if (leftTree.TreeHeight > leftTallestTree)
                {
                    leftTree.Visible = true;
                    leftTallestTree = leftTree.TreeHeight;
                }

                if (rightTree.TreeHeight > rightTallestTree)
                {
                    rightTree.Visible = true;
                    rightTallestTree = rightTree.TreeHeight;
                }

                leftTreeIndex++;
                rightTreeIndex--;
            }
        }


        for (Int32 columnIndex = 0; columnIndex < Trees[0].Count; columnIndex++)
        {
            Int32 topTreeIndex = 0;
            Int32 bottomTreeIndex = Trees[0].Count - 1;

            Int32 topTallestTree = 0;
            Int32 bottomTallestTree = 0;

            Tree topTree = Trees[topTreeIndex][columnIndex];
            Tree bottomTree = Trees[bottomTreeIndex][columnIndex];

            topTree.Visible = true;
            bottomTree.Visible = true;

            while (bottomTreeIndex is not 0)
            {
                topTree = Trees[topTreeIndex][columnIndex];
                bottomTree = Trees[bottomTreeIndex][columnIndex];
                if (topTree.TreeHeight > topTallestTree)
                {
                    topTree.Visible = true;
                    topTallestTree = topTree.TreeHeight;
                }

                if (bottomTree.TreeHeight > bottomTallestTree)
                {
                    bottomTree.Visible = true;
                    bottomTallestTree = bottomTree.TreeHeight;
                }

                topTreeIndex++;
                bottomTreeIndex--;
            }
        }
    }

    public Int32 FindHighestScenicTreeScore()
    {
        Int32 highScore = 0;

        for (Int32 rowIndex = 0; rowIndex < Trees.Count; rowIndex++)
        {
            List<Tree> trees = Trees[rowIndex];
            for (Int32 columnIndex = 0; columnIndex < trees.Count; columnIndex++)
            {
                Tree tree = trees[columnIndex];
                Tree comparisonTree;

                //left
                for (Int32 nextTreeIndex = columnIndex - 1; nextTreeIndex > -1; nextTreeIndex--)
                {
                    comparisonTree = trees[nextTreeIndex];
                    tree.ScenicScoreArray[3]++;
                    if (comparisonTree.TreeHeight >= tree.TreeHeight) break;

                }

                // Right
                for (Int32 nextTreeIndex = columnIndex + 1; nextTreeIndex < trees.Count; nextTreeIndex++)
                {
                    comparisonTree = trees[nextTreeIndex];
                    tree.ScenicScoreArray[1]++;
                    if (comparisonTree.TreeHeight >= tree.TreeHeight) break;

                }



                // Top
                for (Int32 nextTreeIndex = rowIndex - 1; nextTreeIndex > -1; nextTreeIndex--)
                {
                    comparisonTree = Trees[nextTreeIndex][columnIndex];
                    tree.ScenicScoreArray[0]++;
                    if (comparisonTree.TreeHeight >= tree.TreeHeight) break;
                }

                //Bottom
                for (Int32 nextTreeIndex = rowIndex + 1; nextTreeIndex < Trees.Count; nextTreeIndex++)
                {
                    comparisonTree = Trees[nextTreeIndex][columnIndex];
                    tree.ScenicScoreArray[2]++;
                    if (comparisonTree.TreeHeight >= tree.TreeHeight) break;
                }

                if (tree.ScenicScore > highScore)
                {
                    highScore = tree.ScenicScore;
                }
            }
        }

        return highScore;
    }

    public Int32 CalculateTotalVisibleTrees(List<List<Tree>> treeGrid)
    {
        Int32 sum = 0;
        foreach (List<Tree> x in treeGrid)
        {
            Int32 sum1 = 0;
            foreach (Tree tree in x)
            {
                sum1 += tree.Visible ? 1 : 0;
            }

            sum += sum1;
        }

        return sum;
    }
}

//    public Boolean FindTallestVisibleTree(List<List<Tree>> treeGrid, Int32 maxTreeHeight)
//    {
//        Boolean success;
//        success = FindTallestVisibleTreesColumn(treeGrid, maxTreeHeight);
//        success = FindTallestVissibleTreesRow(treeGrid, maxTreeHeight);
//        return success;
//    }
//    public Boolean FindTallestVisibleTreesColumn(List<List<Tree>> treeGrid, Int32 maxTreeHeight)
//    {
//        if (treeGrid.Count <= 1) return false;

//        Int32 columnLength = treeGrid.Count;
//        Int32 rowLength = treeGrid[0].Count;

//        for (Int32 columnIndex = 0; columnIndex < rowLength; columnIndex++)
//        {
//            Tree currentTallest = treeGrid[0][columnIndex];
//            currentTallest.Visible = true;
//            for (Int32 treeIndex = 1; treeIndex < columnLength; treeIndex++)
//            {
//                currentTallest.TallestFromTop = true;

//                Tree comparisonTree = treeGrid[treeIndex][columnIndex];

//                if (currentTallest.TreeHeight > comparisonTree.TreeHeight) continue;

//                currentTallest.TallestFromTop = false;
//                currentTallest = comparisonTree;
//                currentTallest.TallestFromTop = true;
//                currentTallest.Visible = true;

//                if (currentTallest.TreeHeight == maxTreeHeight)
//                    break;
//            }

//            currentTallest = treeGrid[^1][columnIndex];
//            currentTallest.Visible = true;
//            for (Int32 treeIndex = columnLength - 2; treeIndex > -1; treeIndex--)
//            {
//                currentTallest.TallestFromBottom = true;

//                Tree comparisonTree = treeGrid[treeIndex][columnIndex];

//                if (currentTallest.TreeHeight > comparisonTree.TreeHeight) continue;
                
//                currentTallest.TallestFromBottom = false;
//                currentTallest = comparisonTree;
//                currentTallest.TallestFromBottom = true;
//                currentTallest.Visible = true;

//                if (currentTallest.TreeHeight == maxTreeHeight)
//                    break;

//            }
//        }

//        return true;
//    }
//    public Boolean FindTallestVissibleTreesRow(List<List<Tree>> treeGrid, Int32 maxTreeHeight)
//    {
//        foreach (List<Tree> treeRow in treeGrid)
//        {
//            if (!FindTallestVissibleTreesRow(treeRow, 9)) return false;
//        }

//        return true;
//    }
//    public Boolean FindTallestVissibleTreesRow(List<Tree> treeRow, Int32 maxTreeHeight)
//    {
//        if (treeRow.Count <= 1) return false;

//        Tree currentTallest = treeRow[0];
//        Int32 leftTreeIndex = 0;
//        currentTallest.TallestFromLeft = true;
//        currentTallest.Visible = true;
//        for (Int32 i = 1; i < treeRow.Count; i++)
//        {
//            if (currentTallest.TreeHeight > treeRow[i].TreeHeight) continue;
//            currentTallest.TallestFromLeft = false;
//            currentTallest = treeRow[i];
//            currentTallest.TallestFromLeft = true;
//            currentTallest.Visible = true;
//            leftTreeIndex = i;

//            if (currentTallest.TreeHeight == maxTreeHeight)
//                break;
//        }

//        currentTallest = treeRow[^1];
//        currentTallest.TallestFromRight = true;
//        currentTallest.Visible = true;
//        for (Int32 i = treeRow.Count - 2; i > leftTreeIndex-1; i--)
//        {
//            if (currentTallest.TreeHeight > treeRow[i].TreeHeight) continue;
//            currentTallest.TallestFromRight = false;
//            currentTallest = treeRow[i];
//            currentTallest.TallestFromRight = true;
//            currentTallest.Visible = true;

//            if (currentTallest.TreeHeight == maxTreeHeight)
//                break;
//        }

//        return true;
//    }
//}