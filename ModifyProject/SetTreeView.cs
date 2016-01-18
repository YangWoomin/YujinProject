using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccessDB;
using System.Drawing;
using System.Windows.Forms;
using AccessFile;

namespace ModifyProject
{
    public class SetNamespaceTreeView
    {
        private Node nodeList;
        private Node tempNode;
        private TreeView treeView;
        private string fileName;
        private string project;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="treeView"></param>
        /// <param name="fileName"></param>
        /// <param name="project"></param>
        public SetNamespaceTreeView(TreeView treeView, string fileName, string project)
        {
            nodeList = null;
            tempNode = null;
            this.treeView = treeView;
            this.fileName = fileName;
            this.project = project;
            renewTreeView();
        }
        private int addNode(string row)
        {
            try
            {
                tempNode = nodeList;
                while (tempNode.getNext() != null)
                {
                    if (row.ToLower() == tempNode.getPath().ToLower())
                    {
                        return -1;
                    }
                    tempNode = tempNode.getNext();
                }
                if (row.ToLower() == tempNode.getPath().ToLower())
                {
                    return -1;
                }
                Node newNode = new Node(row);
                tempNode.setNext(newNode);
                return 0;
            }
            catch (Exception e)
            {
                return -2;
            }
        }
        private void renewTreeView()
        {
            treeView.Nodes.Clear();
            AccessSqlite sql = new AccessSqlite();
            string[] rows = sql.getRows(project, "name", null);
            int result;
            int i = 0;
            if (rows == null)
            {
                return;
            }
            while (rows.Length > i)
            {
                if (nodeList == null)
                {
                    nodeList = new Node(rows[i]);
                }
                else
                {
                    addNode(rows[i]);
                }
                int point = rows[i].LastIndexOf(".");
                while (point != -1)
                {
                    rows[i] = rows[i].Substring(0, point);
                    result = addNode(rows[i]);
                    if(result != 0)
                    {
                        break;
                    }
                    point = rows[i].LastIndexOf(".");
                }
                i++;
            }

            tempNode = nodeList;
            while (tempNode != null)
            {
                string pPath = tempNode.getPath();
                int point = pPath.LastIndexOf(".");
                if (point != -1)
                {
                    pPath = pPath.Substring(0, point);
                }
                else
                {
                    pPath = "";
                }
                Node tempNode2 = nodeList;
                while (tempNode2 != null)
                {
                    if (tempNode2.getPath().ToLower() == pPath.ToLower())
                    {
                        tempNode2.getNode().Nodes.Add(tempNode.getNode());
                        break;
                    }
                    tempNode2 = tempNode2.getNext();
                }
                tempNode = tempNode.getNext();
            }

            tempNode = nodeList;
            while (tempNode != null)
            {
                if (tempNode.getNode().Text.ToLower() == tempNode.getPath().ToLower())
                {
                    treeView.Nodes.Add(tempNode.getNode());
                }
                tempNode = tempNode.getNext();
            }
        }
        public string getPath()
        {
            try
            {
                string selNode;
                string path;
                TreeNode selectedNode;
                selNode = treeView.SelectedNode.Text;
                selectedNode = treeView.SelectedNode;
                path = selectedNode.Text;
                while (selectedNode.Parent != null)
                {
                    selectedNode = selectedNode.Parent;
                    path = selectedNode.Text + "." + path;
                }
                return path;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public string getParentPath()
        {
            try
            {
                string path;
                TreeNode tempNode = treeView.SelectedNode.Parent;
                path = tempNode.Text;
                while(tempNode.Parent != null)
                {
                    tempNode = tempNode.Parent;
                    path = tempNode.Text + "." + path;
                }
                return path;
            }
            catch(Exception e)
            {
                return null;
            }
        }
        public void expandNode(string path)
        {
            int point = path.LastIndexOf(".");
            if(point != -1)
            {
                path = path.Substring(0, point);
            }
            else
            {
                return;
            }
            tempNode = nodeList;
            TreeNode theNode = null;
            if (path.ToLower() == tempNode.getPath().ToLower())
            {
                theNode = tempNode.getNode();
            }
            while (tempNode.getNext() != null)
            {
                tempNode = tempNode.getNext();
                if (path.ToLower() == tempNode.getPath().ToLower())
                {
                    theNode = tempNode.getNode();
                }
            }
            theNode.Expand();
            try
            {
                while (theNode.Parent != null)
                {
                    theNode = theNode.Parent;
                    theNode.Expand();
                }
            }
            catch (Exception e)
            {
            }
        }
    }

    public class SetClassTreeView
    {
        private TreeView treeView;
        private string fileName;
        private string project;
        private string path;

        public SetClassTreeView(TreeView treeView, string fileName, string project, string path)
        {
            this.treeView = treeView;
            this.fileName = fileName;
            this.project = project;
            this.path = path;
            renewTreeView();
        }
        private void renewTreeView()
        {
            treeView.Nodes.Clear();
            AccessSqlite sql = new AccessSqlite();
            int point;
            string[] rows = sql.getRows(project, "class", "name = '" + path + "'");
            int i = 0;
            try
            {
                if (rows == null || rows[0] == "")
                    return;
                while (rows.Length > i)
                {
                    treeView.Nodes.Add(rows[i]);
                    treeView.Nodes[i].BackColor = Color.DarkViolet;
                    treeView.Nodes[i].ForeColor = Color.White;
                    string[] row = sql.getRows(project, "field", "name = '" + path + "' and class = '" + rows[i] + "'");
                    if (row != null && row[0] != "" && row[0] != null)
                    {
                        point = row[0].IndexOf(",");
                        while (point != -1)
                        {
                            string temp = row[0].Substring(0, point);
                            int point2 = temp.IndexOf("-");
                            temp = temp.Substring(0, point2) + "(" + temp.Substring(point2 + 1, point - point2 - 1) + ")";
                            treeView.Nodes[i].Nodes.Add(temp);
                            row[0] = row[0].Substring(point + 1);
                            point = row[0].IndexOf(",");
                        }
                    }
                    i++;
                }
            }
            catch(Exception e)
            {
            }
        }
        public string getClassName()
        {
            try
            {
                TreeNode tempNode = treeView.SelectedNode;
                while (tempNode.Parent != null)
                {
                    tempNode = tempNode.Parent;
                }
                return tempNode.Text;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public string getFieldName()
        {
            try
            {
                if (treeView.SelectedNode.Parent == null)
                {
                    return null;
                }
                else
                {
                    return treeView.SelectedNode.Text;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public void expandNode(string className)
        {
            int i = 0;
            
            while(treeView.Nodes.Count > i)
            {
                if(treeView.Nodes[i].Text == className)
                {
                    treeView.Nodes[i].Expand();
                    break;
                }
                i++;
            }
        }
    }

    public class Node
    {
        private string path;
        private TreeNode node;
        private Node next;

        public Node(string str)
        {
            path = str;
            int point = str.LastIndexOf(".");
            if (point == -1)
            {
                node = new TreeNode(str);
            }
            else
            {
                node = new TreeNode(str.Substring(point + 1));
            }
            next = null;
        }
        public TreeNode getNode()
        {
            return node;
        }
        public Node getNext()
        {
            return next;
        }
        public void setNext(Node newNode)
        {
            next = newNode;
        }
        public string getPath()
        {
            return path;
        }
    }
}
