using System;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(String[] args) {
            Node root = null;
            int T = Int32.Parse(Console.ReadLine());
            while(T-- > 0) {
                int data = Int32.Parse(Console.ReadLine());
                root = insert(root, data);            
            }
            int height = getHeight(root);
            Console.WriteLine("bst height is: ");
            Console.WriteLine(height);
            Console.ReadLine();
        }

        static int getHeight(Node root) {
            if(root == null) {
                return -1;
            }
            var left = getHeight(root.left) + 1;
            var right = getHeight(root.right) + 1;
            return Math.Max(left, right);
        }

        static Node insert(Node root, int data) {
            if(root == null) {
                return new Node(data);
            }
            else {
                Node cur;
                if(data <= root.data) {
                    cur = insert(root.left,data);
                    root.left = cur;
                }
                else {
                    cur = insert(root.right,data);
                    root.right = cur;
                }
                return root;
            }
        }
    }

    class Node {
        public Node left, right;
        public int data;
        public Node(int data){
            this.data = data;
            left = right = null;
        }
    }
}
/* console input
7
3
5
2
1
4
6
7
*/

/*
console output should be 3
*/