using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Red_Black_Tree
{
    public class TreeNode
    {
        public enum Color
        {
            Red,
            Black
        };

        public Color colour;
        public int blackLevel = 0;
        public bool phantomLeaf = false;
        public int canbang;     // Hệ số cân bằng
        public int number;      // Giá trị node
        public TreeNode left;       // Node pLeft
        public TreeNode right;      // Node pRight
        public TreeNode parent;
        public PointF vitri;    // Vị trí hiện tại của node
        public PointF locationOld;    // Vị trí cũ - dùng cho việc di chuyển cây
        public PointF locationNew;    // Vị trí mới - dùng cho việc di chuyển cây
                                      // Hàm tạo không đối số

        public TreeNode treeRoot = null;

        public TreeNode getRoot()
        {
            return this.treeRoot;
        }

        public void clearRoot()
        {
            this.treeRoot = null;
        }
        public TreeNode()
        {
            //this.canbang = 0;
            //this.left = null;
            //this.right = null;
            //this.parent = null;
            this.vitri.X = 512;
            this.vitri.Y = 20;
            this.locationOld.X = 512;
            this.locationOld.Y = 20;
        }
        // Hàm tạo có đối số là giá trị node
        public TreeNode(int number)
        {
            this.canbang = 0;
            this.number = number;
            this.left = null;
            this.right = null;
            this.parent = null;
            this.vitri.X = 512;
            this.vitri.Y = 20;
            this.locationOld.X = 512;
            this.locationOld.Y = 20;
        }

        public TreeNode(Color colour)
        {
            this.canbang = 0;
            this.left = null;
            this.right = null;
            this.parent = null;
            this.vitri.X = 512;
            this.vitri.Y = 20;
            this.locationOld.X = 512;
            this.locationOld.Y = 20;
            this.colour = colour;
        }

        public TreeNode(int number, Color colour)
        {
            this.canbang = 0;
            this.left = null;
            this.right = null;
            this.parent = null;
            this.vitri.X = 512;
            this.vitri.Y = 20;
            this.locationOld.X = 512;
            this.locationOld.Y = 20;
            this.colour = colour;
            this.number = number;
        }

        bool isLeftChild()
        {
            if (this.parent == null)
            {
                return true;
            }

            return this.parent.left == this;
        }

        int getBlackLevel(TreeNode tree)
        {
            if (tree == null)
            {
                return 1;
            }
            else
            {
                return tree.blackLevel;
            }
        }

        void fixNodeColor(TreeNode tree)
        {
            if (tree.blackLevel == 0)
            {
                tree.colour = Color.Red;
            }
            else
            {
                tree.colour = Color.Black;
            }
        }

        void fixExtraBlackChild(TreeNode parNode, bool isLeftChild)
        {
            TreeNode sibling;
            TreeNode doubleBlackNode;
            if (isLeftChild)
            {
                sibling = parNode.right;
                doubleBlackNode = parNode.left;
            }
            else
            {
                sibling = parNode.left;
                doubleBlackNode = parNode.right;
            }
            if (this.getBlackLevel(sibling) > 0 && this.getBlackLevel(sibling.left) > 0 && this.getBlackLevel(sibling.right) > 0)
            {
                sibling.blackLevel = 0;
                this.fixNodeColor(sibling);
                if (doubleBlackNode != null)
                {
                    doubleBlackNode.blackLevel = 1;
                    this.fixNodeColor(doubleBlackNode);

                }
                if (parNode.blackLevel == 0)
                {
                    parNode.blackLevel = 1;
                    this.fixNodeColor(parNode);
                }
                else
                {
                    parNode.blackLevel = 2;
                    this.fixNodeColor(parNode);
                    this.fixExtraBlack(parNode);
                }
            }
            else if (this.getBlackLevel(sibling) == 0)
            {
                if (isLeftChild)
                {
                    var newPar = this.singleRotateLeft(parNode);
                    newPar.blackLevel = 1;
                    this.fixNodeColor(newPar);
                    newPar.left.blackLevel = 0;
                    this.fixNodeColor(newPar.left);
                    this.fixExtraBlack(newPar.left.left);

                }
                else
                {
                    var newPar = this.singleRotateRight(parNode);
                    newPar.blackLevel = 1;
                    this.fixNodeColor(newPar);
                    newPar.right.blackLevel = 0;
                    this.fixNodeColor(newPar.right);

                    this.fixExtraBlack(newPar.right.right);
                }
            }
            else if (isLeftChild && this.getBlackLevel(sibling.right) > 0)
            {
                var newSib = this.singleRotateRight(sibling);
                newSib.blackLevel = 1;
                this.fixNodeColor(newSib);
                newSib.right.blackLevel = 0;
                this.fixNodeColor(newSib.right);
                this.fixExtraBlackChild(parNode, isLeftChild);
            }
            else if (!isLeftChild && this.getBlackLevel(sibling.left) > 0)
            {
                var newSib = this.singleRotateLeft(sibling);
                newSib.blackLevel = 1;
                this.fixNodeColor(newSib);
                newSib.left.blackLevel = 0;
                this.fixNodeColor(newSib.left);
                this.fixExtraBlackChild(parNode, isLeftChild);
            }
            else if (isLeftChild)
            {
                var oldParBlackLevel = parNode.blackLevel;
                var newPar = this.singleRotateLeft(parNode);
                if (oldParBlackLevel == 0)
                {
                    newPar.blackLevel = 0;
                    this.fixNodeColor(newPar);
                    newPar.left.blackLevel = 1;
                    this.fixNodeColor(newPar.left);
                }
                newPar.right.blackLevel = 1;
                this.fixNodeColor(newPar.right);
                if (newPar.left.left != null)
                {
                    newPar.left.left.blackLevel = 1;
                    this.fixNodeColor(newPar.left.left);
                }
            }
            else
            {
                var oldParBlackLevel = parNode.blackLevel;
                var newPar = this.singleRotateRight(parNode);
                if (oldParBlackLevel == 0)
                {
                    newPar.blackLevel = 0;
                    this.fixNodeColor(newPar);
                    newPar.right.blackLevel = 1;
                    this.fixNodeColor(newPar.right);
                }
                newPar.left.blackLevel = 1;
                this.fixNodeColor(newPar.left);
                if (newPar.right.right != null)
                {
                    newPar.right.right.blackLevel = 1;
                    this.fixNodeColor(newPar.right.right);
                }
            }
        }

        private TreeNode singleRotateRight(TreeNode tree)
        {
            var B = tree;
            var t3 = B.right;
            var A = tree.left;
            var t1 = A.left;
            var t2 = A.right;

            if (t2 != null)
            {
                t2.parent = B;
            }

            A.parent = B.parent;
            if (this.treeRoot == B)
            {
                this.treeRoot = A;
            }
            else
            {
                if (B.isLeftChild())
                {
                    B.parent.left = A;
                }
                else
                {
                    B.parent.right = A;
                }
            }
            A.right = B;
            B.parent = A;
            B.left = t2;

            return A;
        }

        private TreeNode singleRotateLeft(TreeNode tree)
        {
            var A = tree;
            var B = tree.right;
            var t1 = A.left;
            var t2 = B.left;
            var t3 = B.right;

            if (t2 != null)
            {
                t2.parent = A;
            }
            B.parent = A.parent;
            if (this.treeRoot == A)
            {
                this.treeRoot = B;
            }
            else
            {
                if (A.isLeftChild())
                {
                    A.parent.left = B;
                }
                else
                {
                    A.parent.right = B;
                }
            }
            B.left = A;
            A.parent = B;
            A.right = t2;
            return B;
        }

        private void fixExtraBlack(TreeNode tree)
        {
            if (tree.blackLevel > 1)
            {
                if (tree.parent == null)
                {
                    tree.blackLevel = 1;
                }
                else if (tree.parent.left == tree)
                {
                    this.fixExtraBlackChild(tree.parent, true);
                }
                else
                {
                    this.fixExtraBlackChild(tree.parent, false);
                }

            }
            else
            {
                // No extra blackness
            }
        }

        void fixLeftNull(TreeNode tree)
        {
            TreeNode nullLeaf = new TreeNode();
            nullLeaf.blackLevel = 2;
            nullLeaf.parent = tree;
            nullLeaf.phantomLeaf = true;
            tree.left = nullLeaf;

            this.fixExtraBlackChild(tree, true);
            nullLeaf.blackLevel = 1;
            this.fixNodeColor(nullLeaf);
        }

        public void deleteElement (int deletedValue)
        {
            this.treeDelete(this.treeRoot, deletedValue);
        }

        void treeDelete(TreeNode tree, int valueToDelete)
        {
            bool leftchild = false;
            if (tree != null && !tree.phantomLeaf)
            {
                if (tree.parent != null)
                {
                    leftchild = tree.parent.left == tree;
                }

                if (valueToDelete == tree.number)
                {
                    var needFix = tree.blackLevel > 0;
                    if (((tree.left == null) || tree.left.phantomLeaf) && ((tree.right == null) || tree.right.phantomLeaf))
                    {
                        if (leftchild && tree.parent != null)
                        {
                            tree.parent.left = null;

                            if (needFix)
                            {
                                this.fixLeftNull(tree.parent);
                            }
                            else
                            {
                                this.attachLeftNullLeaf(tree.parent);
                            }
                        }
                        else if (tree.parent != null)
                        {
                            tree.parent.right = null;
                            if (needFix)
                            {
                                this.fixRightNull(tree.parent);
                            }
                            else
                            {
                                this.attachRightNullLeaf(tree.parent);
                            }
                        }
                        else
                        {
                            this.treeRoot = null;
                        }

                    }
                    else if (tree.left == null || tree.left.phantomLeaf)
                    {
                        if (tree.left != null)
                        {
                            tree.left = null;
                        }

                        if (tree.parent != null)
                        {
                            if (leftchild)
                            {
                                tree.parent.left = tree.right;
                                if (needFix)
                                {
                                    tree.parent.left.blackLevel++;
                                    this.fixNodeColor(tree.parent.left);
                                    this.fixExtraBlack(tree.parent.left);
                                }
                            }
                            else
                            {
                                tree.parent.right = tree.right;
                                if (needFix)
                                {
                                    tree.parent.right.blackLevel++;
                                    this.fixNodeColor(tree.parent.right);
                                    this.fixExtraBlack(tree.parent.right);
                                }

                            }
                            tree.right.parent = tree.parent;
                        }
                        else
                        {
                            this.treeRoot = tree.right;
                            this.treeRoot.parent = null;
                            if (this.treeRoot.blackLevel == 0)
                            {
                                this.treeRoot.blackLevel = 1;
                            }
                        }
                    }
                    else if (tree.right == null || tree.right.phantomLeaf)
                    {
                        if (tree.right != null)
                        {
                            tree.right = null;
                        }
                        if (tree.parent != null)
                        {
                            if (leftchild)
                            {
                                tree.parent.left = tree.left;
                                if (needFix)
                                {
                                    tree.parent.left.blackLevel++;
                                    this.fixNodeColor(tree.parent.left);
                                    this.fixExtraBlack(tree.parent.left);
                                }
                                else
                                {
                                }
                            }
                            else
                            {
                                tree.parent.right = tree.left;
                                if (needFix)
                                {
                                    tree.parent.right.blackLevel++;
                                    this.fixNodeColor(tree.parent.right);
                                    this.fixExtraBlack(tree.parent.left);
                                }
                                else
                                {
                                }
                            }
                            tree.left.parent = tree.parent;
                        }
                        else
                        {
                            this.treeRoot = tree.left;
                            this.treeRoot.parent = null;
                            if (this.treeRoot.blackLevel == 0)
                            {
                                this.treeRoot.blackLevel = 1;
                                this.fixNodeColor(this.treeRoot);
                            }
                        }
                    }
                    else // tree.left != null && tree.right != null
                    {
                        var tmp = tree;
                        tmp = tree.left;
                        while (tmp.right != null && !tmp.right.phantomLeaf)
                        {
                            tmp = tmp.right;
                        }
                        if (tmp.right != null)
                        {
                            tmp.right = null;
                        }
                        tree.number = tmp.number;

                        needFix = tmp.blackLevel > 0;


                        if (tmp.left == null)
                        {
                            if (tmp.parent != tree)
                            {
                                tmp.parent.right = null;
                                if (needFix)
                                {
                                    this.fixRightNull(tmp.parent);
                                }
                                else
                                {
                                }
                            }
                            else
                            {
                                tree.left = null;
                                if (needFix)
                                {
                                    this.fixLeftNull(tmp.parent);
                                }
                                else
                                {
                                }
                            }
                        }
                        else
                        {
                            if (tmp.parent != tree)
                            {
                                tmp.parent.right = tmp.left;
                                tmp.left.parent = tmp.parent;

                                if (needFix)
                                {
                                    tmp.left.blackLevel++;
                                    if (tmp.left.phantomLeaf)
                                    {
                                    }
                                    this.fixNodeColor(tmp.left);
                                    this.fixExtraBlack(tmp.left);
                                    if (tmp.left.phantomLeaf)
                                    {
                                    }

                                }
                                else
                                {
                                }
                            }
                            else
                            {
                                tree.left = tmp.left;
                                tmp.left.parent = tree;
                                if (needFix)
                                {
                                    tmp.left.blackLevel++;
                                    if (tmp.left.phantomLeaf)
                                    {
                                    }

                                    this.fixNodeColor(tmp.left);
                                    this.fixExtraBlack(tmp.left);
                                    if (tmp.left.phantomLeaf)
                                    {
                                    }

                                }
                                else
                                {
                                }
                            }
                        }
                        tmp = tmp.parent;

                    }
                }
                else if (valueToDelete < tree.number)
                {
                    if (tree.left != null)
                    {
                    }
                    this.treeDelete(tree.left, valueToDelete);
                }
                else
                {
                    if (tree.right != null)
                    {
                    }
                    this.treeDelete(tree.right, valueToDelete);
                }
            }
            else
            {
            }
        }

        private void attachRightNullLeaf(TreeNode node)
        {
            node.right = new TreeNode();
            node.right.colour = Color.Black;
            node.right.phantomLeaf = true;
            node.right.blackLevel = 1;
        }

        public void insertElement(int insertedValue)
        {
            if (this.treeRoot == null)
            {
                this.treeRoot = new TreeNode(insertedValue);
                this.treeRoot.blackLevel = 1;

                this.attachNullLeaves(this.treeRoot);
            }
            else
            {
                var insertElem = new TreeNode(insertedValue);
                insertElem.colour = Color.Red;
                this.insert(insertElem, this.treeRoot);
            }
        }

        private void insert(TreeNode elem, TreeNode tree)
        {
            if (elem.number < tree.number)
            {
                if (tree.left == null || tree.left.phantomLeaf)
                {
                    if (tree.left != null)
                    {
                    }
                    tree.left = elem;
                    elem.parent = tree;

                    this.attachNullLeaves(elem);

                    this.fixDoubleRed(elem);
                }
                else
                {
                    this.insert(elem, tree.left);
                }
            }
            else
            {
                if (tree.right == null || tree.right.phantomLeaf)
                {
                    if (tree.right != null)
                    {
                    }
                    tree.right = elem;
                    elem.parent = tree;

                    this.attachNullLeaves(elem);

                    this.fixDoubleRed(elem);
                }
                else
                {
                    this.insert(elem, tree.right);
                }
            }
        }

        private void fixDoubleRed(TreeNode tree)
        {
            if (tree.parent != null)
            {
                if (tree.parent.blackLevel > 0)
                {
                    return;
                }
                if (tree.parent.parent == null)
                {
                    tree.parent.blackLevel = 1;
                    tree.parent.colour = Color.Black;
                    return;
                }
                var uncle = this.findUncle(tree);
                if (this.getBlackLevel(uncle) == 0)
                {
                    uncle.colour = Color.Black;
                    uncle.blackLevel = 1;

                    tree.parent.blackLevel = 1;
                    tree.parent.colour = Color.Black;

                    tree.parent.parent.blackLevel = 0;
                    tree.parent.parent.colour = Color.Red;
                    this.fixDoubleRed(tree.parent.parent);
                }
                else
                {
                    if (tree.isLeftChild() && !tree.parent.isLeftChild())
                    {
                        this.singleRotateRight(tree.parent);
                        tree = tree.right;

                    }
                    else if (!tree.isLeftChild() && tree.parent.isLeftChild())
                    {
                        this.singleRotateLeft(tree.parent);
                        tree = tree.left;
                    }

                    if (tree.isLeftChild())
                    {
                        this.singleRotateRight(tree.parent.parent);
                        tree.parent.blackLevel = 1;
                        tree.parent.colour = Color.Black;

                        tree.parent.right.blackLevel = 0;
                        tree.parent.right.colour = Color.Red;
                    }
                    else
                    {
                        this.singleRotateLeft(tree.parent.parent);
                        tree.parent.blackLevel = 1;
                        tree.parent.colour = Color.Black;

                        tree.parent.left.blackLevel = 0;
                        tree.parent.left.colour = Color.Red;
                    }
                }

            }
            else
            {
                if (tree.blackLevel == 0)
                {
                    tree.blackLevel = 1;
                    tree.colour = Color.Black;
                }
            }
        }

        private TreeNode findUncle(TreeNode tree)
        {
            if (tree.parent == null)
            {
                return null;
            }
            var par = tree.parent;
            if (par.parent == null)
            {
                return null;
            }
            var grandPar = par.parent;

            if (grandPar.left == par)
            {
                return grandPar.right;
            }
            else
            {
                return grandPar.left;
            }
        }

        private void attachNullLeaves(TreeNode node)
        {
            this.attachLeftNullLeaf(node);
            this.attachRightNullLeaf(node);
        }

        private void fixRightNull(TreeNode tree)
        {
            var nullLeaf = new TreeNode();

            nullLeaf.colour = Color.Black;
            nullLeaf.parent = tree;
            nullLeaf.phantomLeaf = true;
            nullLeaf.blackLevel = 2;
            tree.right = nullLeaf;

            this.fixExtraBlackChild(tree, false);

            nullLeaf.blackLevel = 1;
            this.fixNodeColor(nullLeaf);
        }

        private void attachLeftNullLeaf(TreeNode node)
        {
            node.left = new TreeNode();
            node.left.phantomLeaf = true;
            node.left.blackLevel = 1;
        }
    }
}
