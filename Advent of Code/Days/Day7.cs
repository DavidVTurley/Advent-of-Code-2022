using System;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Advent_of_Code.Days;

public class Day7 : IDay
{
    public async Task Setup(HttpClient client)
    {
        String input = await ExtraFunctions.MakeAdventOfCodeInputRequest(client, 7);
        input = $"$ cd /\n$ ls\ndir a\n14848514 b.txt\n8504156 c.dat\ndir d\n$ cd a\n$ ls\ndir e\n29116 f\n2557 g\n62596 h.lst\n$ cd e\n$ ls\n584 i\n$ cd ..\n$ cd ..\n$ cd d\n$ ls\n4060174 j\n8033020 d.log\n5626152 d.ext\n7214296 k";
        commands = input.Split('\n', StringSplitOptions.RemoveEmptyEntries).ToList();
        _node = CreateNodeTree();
    }

    private List<String> commands = new();
    private Node _node = new Node("/");
    public void Challenge1()
    {
        _node.PrintTree();

        //if (_node.Children[0].)

        _node.Children.RemoveRange(1, 3);
        Dictionary<String, Int32> dirList = _node.CalculateDirectorySizes();
        var s = dirList.Where(x => x.Value <= 100000).Sum(x => x.Value);
        Console.WriteLine(s);
    }

    public void Challenge2()
    {
        throw new NotImplementedException();
    }

    internal Node CreateNodeTree()
    {
        Node baseNode = new Node("/");
        Node currentNode = baseNode;

        foreach (String command in commands)
        {
            String action = String.Empty;
            String actionValue = String.Empty;
            if (command[0] == '$')
            {
                action = command.Substring(2, 2);
                if (action != "ls") actionValue = command.Substring(5);
                switch (action)
                {
                    case "cd":
                        switch (actionValue)
                        {
                            case "/":
                                currentNode = baseNode;
                                break;
                            case "..":
                                currentNode = currentNode.Parent;
                                break;
                            default:
                                Node? childNode = currentNode.ContainsChild(actionValue);
                                if (childNode == null)
                                {
                                    Node newNode = new Node(actionValue, 0, currentNode);
                                    currentNode.AddChildNode(newNode);
                                    childNode = newNode;
                                }
                                currentNode = childNode;

                                break;
                        }
                        break;
                    case "ls":
                        //baseNode.PrintTree();
                        break;
                }
            }
            else
            {
                action += command.Substring(0, 3);
                Node? node = null;
                switch (action)
                {
                    case "dir":
                        actionValue += command.Substring(4);
                        node = currentNode.ContainsChild(actionValue);
                        if (node == null)
                        {
                            currentNode.AddChildNode(actionValue, 0, currentNode);
                        }
                        break;
                    default:
                        String[] s = command.Split(' ');
                        action = s[0];
                        actionValue = s[1];

                        node = currentNode.ContainsChild(actionValue);
                        if (node == null)
                        {
                            node = currentNode.AddChildNode(actionValue, Int32.Parse(action), currentNode);
                        }

                        node.FileSize = Int32.Parse(action);

                        break;
                }

            }
        }

        return baseNode;
    }
    internal class Node
    {
        public String Name;
        public Int32 FileSize;

        public Node? Parent { get; }
        public List<Node> Children { get; }
        public Int32 Depth { get; }

        public Node(String name, Int32 fileSize = 0, Node? parent = null!, List<Node>? children = null!)
        {
            Name = name;
            Parent = parent;
            FileSize = fileSize;
            Children = new List<Node>();
            if (children != null)
            {
                foreach (Node child in children)
                {
                    AddChildNode(child);
                }
            }

            Depth = parent == null ? 0 : parent.Depth + 1;
        }

        public void PrintTree()
        {
            String indent = new (' ', Depth);
            Console.WriteLine(indent + Name +  ": " + FileSize);

            foreach (Node child in Children)
            {
                child.PrintTree();
            }
        }

        public Node AddChildNode(String name, Int32 value, Node parent = null, List<Node> children = null)
        {
            Node newChild = new Node(name, value, this, children ?? new List<Node>());
            Children.Add(newChild);
            return newChild;
        }

        public Node AddChildNode(Node node)
        {
            AddChildNode(node.Name, node.FileSize, this, node.Children);
            return node;
        }

        public void RemoveChild(Node node)
        {
            Children.Remove(node);
        }

        public Node? ContainsChild(String name)
        {
            return Children.Find(n => n.Name == name);
        }

        public Dictionary<String, Int32> CalculateDirectorySizes()
        {
            String aaa = Name;
            Int32 count = 0;
            Dictionary<String, Int32> dirList = new();
            foreach (Node child in Children)
            {
                var qaa = child.Name;
                if (child.FileSize == 0)
                {
                    foreach (KeyValuePair<String, Int32> dir in child.CalculateDirectorySizes())
                    {
                        dirList.Add(dir.Key, dir.Value);
                    }

                    count += dirList[child.Name];

                }
                else count += child.FileSize;

            }

            dirList.Add(Name, count);
            return dirList;
        }

    }
}