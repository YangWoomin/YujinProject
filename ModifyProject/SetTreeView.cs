using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccessDB;
using System.Drawing;
using System.Windows.Forms;

namespace ModifyProject
{
    public class SetNamespaceTreeView
    {
        private Node nodeList;
        private Node tempNode;
        private TreeView treeView;
        private string fileName;
        private string project;

        public SetNamespaceTreeView(TreeView treeView, string fileName, string project)
        {
            nodeList = null;
            tempNode = null;
            this.treeView = treeView;
            this.fileName = fileName;
            this.project = project;
            renewTreeView();
        }
        private void addNode(string row)
        {
            try
            {
                int flag = 0;
                tempNode = nodeList;
                while (tempNode.getNext() != null)
                {
                    if (row == tempNode.getPath())
                    {
                        flag = 1;
                        break;
                    }
                    tempNode = tempNode.getNext();
                }
                if (row == tempNode.getPath())
                {
                    flag = 1;
                }
                if (flag == 0)
                {
                    Node newNode = new Node(row);
                    tempNode.setNext(newNode);
                }
            }
            catch (Exception e)
            {
                return;
            }
        }
        private void renewTreeView()
        {
            treeView.Nodes.Clear();
            AccessSqlite sql = new AccessSqlite(fileName);
            string[] rows = sql.getRows(project, "name", null);
            int i = 0;
            if (rows == null)
                return;
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
                    addNode(rows[i]);
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
                    if (string.Compare(tempNode2.getPath(), pPath) == 0)
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
                if (string.Compare(tempNode.getNode().Text, tempNode.getPath()) == 0)
                {
                    treeView.Nodes.Add(tempNode.getNode());
                }
                tempNode = tempNode.getNext();
            }
            nodeList = null;
            tempNode = null;
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
            AccessSqlite sql = new AccessSqlite(fileName);
            string[] rows = sql.getRows(project, "class", "name = '" + path + "'");
            int i = 0;
            if (rows == null || rows[0] == "")
                return;
            while (rows.Length > i)
            {
                treeView.Nodes.Add(rows[i]);
                treeView.Nodes[i].BackColor = Color.Blue;
                treeView.Nodes[i].ForeColor = Color.White;
                string[] row = sql.getRows(project, "field", "name = '" + path + "' and class = '" + rows[i] + "'");
                if (row != null && row[0] != "")
                {
                    int point = row[0].IndexOf(",");
                    while (point != -1)
                    {
                        treeView.Nodes[i].Nodes.Add(row[0].Substring(0, point));
                        row[0] = row[0].Substring(point + 1);
                        point = row[0].IndexOf(",");
                    }
                    treeView.Nodes[i].Nodes.Add(row[0]);
                }
                i++;
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
