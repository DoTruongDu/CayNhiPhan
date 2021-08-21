using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace SimuBTree
{
  public partial class CayBTree : Form, Helper.HelperTraceListener
  {
    int Ordre;
    BTreeClass BTree;

    public CayBTree()
    {
      InitializeComponent();
      Helper.EnregistreListener(this);
    }

    private void btAjouter_Click(object sender, EventArgs e)
    {
      try
    {
        BTree = null;
        Ordre = int.Parse("5");
        int nb = int.Parse(tbNb.Text);
        int max = int.Parse(tbMax.Text);
        if (BTree == null || BTree.Order != Ordre)
        {
          BTree = new BTreeClass(Ordre);
        }
        List<int> v = new List<int>(max);
        for (int i = nb; i < max; i++)
        {
          v.Add(i);
          bool bOK = BTree.Add(i);
        }
        int seed = 5;
        Random rnd = new Random(seed);
        for (int i = 0; i < nb; i++)
        {
          int j = rnd.Next(v.Count);
          int n = v[j];
          v.RemoveAt(j);
          bool bOK = BTree.Add(n);
          Helper.Assert(bOK);
        }
        ShowTreeView();
        ScanBTree(BTree);
      }
      catch (ApplicationException ex)
      {
        Trace(ex.Message);
        BTree = null;
      }
      catch (Exception ex)
      {
        Trace(ex.ToString());
        BTree = null;
      }
    }

    private void btSupprimer_Click(object sender, EventArgs e)
    {
      TreeNode node = BTreeView.SelectedNode;
      if (node == null)
      {
        return;
      }
      string text = node.Text;
      if (!int.TryParse(text, out int value))
      {
        return;
      }
      bool bOK = BTree.Remove(value);
      ShowTreeView();
      ScanBTree(BTree);
    }

    private void ShowTreeView()
    {
      BTreeView.SuspendLayout();
      BTreeNode root = BTree.Root;
      bool children = !root.Leaf;
      BTreeView.Nodes.Clear();
      // TODO : 
      if (children)
      {
        TreeNode node = new TreeNode("-");
        BTreeView.Nodes.Add(node);
        ShowTreeView(node, root.Child(0));
      }
      for (int i = 0; i < root.NbKeys; i++)
      {
        TreeNode node = new TreeNode(root.Key(i).ToString());
        BTreeView.Nodes.Add(node);
        if (children)
        {
          ShowTreeView(node, root.Child(i + 1));
        }
      }
      BTreeView.ExpandAll();
      BTreeView.ResumeLayout();
    }

    private void ShowTreeView(TreeNode parent, BTreeNode root)
    {
      bool children = !root.Leaf;
      if (children)
      {
        TreeNode node = new TreeNode("-");
        parent.Nodes.Add(node);
        ShowTreeView(node, root.Child(0));
      }
      for (int i = 0; i < root.NbKeys; i++)
      {
        TreeNode node = new TreeNode(root.Key(i).ToString());
        parent.Nodes.Add(node);
        if (children)
        {
          ShowTreeView(node, root.Child(i + 1));
        }
      }
    }

    private void ScanBTree(BTreeClass bTree)
    {
      bTree.Scan();
    }

    private void btMassAdd_Click(object sender, EventArgs e)
    {
      try
      {
        Ordre = int.Parse("5");
        if (Ordre < 2)
        {
        }
        BTreeClass BTree = new BTreeClass(Ordre);
        // valeurs entre 0 et max-1
        int max = 1_000_000_000;
        // Nombre de tentatives d'insertions
        // Le nombre final d'insertions est inférieur 
        // si random.Next() renvoie plusieurs fois une même valeur
        int nb = 60_000_000;
        int seed = 0;
        Random random = new Random(seed);
        // Random random = new Random();
        Stopwatch stopwatch = Stopwatch.StartNew();
        for (int i = 0; i < nb; i++)
        {
          BTree.Add(random.Next(max));
        }
        stopwatch.Stop();
        stopwatch.Restart();
        ScanBTree(BTree);
      }
      catch (ApplicationException ex)
      {
        Trace(ex.Message);
      }
      catch (Exception ex)
      {
        Trace(ex.ToString());
      }
    }

    delegate void TraceDelegate(string msg);

    public void Trace(string msg)
    {
      if (InvokeRequired)
      {
        Invoke(new TraceDelegate(Trace), msg);
        return;
      }
    }

    // Vagues successives d'ajouts et suppressions dans un BTree
    private void btAddSuppr_Click(object sender, EventArgs e)
    {
      try
      {
        Ordre = int.Parse("5");
        int nb = int.Parse(tbNb.Text);
        if (nb < 1)
        {
        }
        int max = int.Parse(tbMax.Text);
        // On part d'un arbre vide
        BTree = new BTreeClass(Ordre);
        List<int> valeursDispo = new List<int>(max);
        List<int> valeursIncluses = new List<int>(nb);
        for (int i = 0; i < max; i++)
        {
          valeursDispo.Add(i);
        }
        int seed = 5;
        Random rnd = new Random(seed);
        for (int i = 0; i < nb; i++)
        {
          int j = rnd.Next(valeursDispo.Count);
          int n = valeursDispo[j];
          valeursDispo.RemoveAt(j);
          valeursIncluses.Add(n);
          bool bOK = BTree.Add(n);
          Helper.Assert(bOK);
        }
        while (BTree.NbKeys > 0)
        {
          int S = BTree.NbKeys / 2;
          if (S == 0)
          {
            S = 1;
          }
          for (int i = 0; i < S; i++)
          {
            int j = rnd.Next(valeursIncluses.Count);
            int n = valeursIncluses[j];
            valeursIncluses.RemoveAt(j);
            valeursDispo.Add(n);
            bool bOK = BTree.Remove(n);
            Helper.Assert(bOK);
          }
          int N = S / 2;
          for (int i = 0; i < N; i++)
          {
            int j = rnd.Next(valeursDispo.Count);
            int n = valeursDispo[j];
            valeursDispo.RemoveAt(j);
            valeursIncluses.Add(n);
            bool bOK = BTree.Add(n);
            Helper.Assert(bOK);
          }
        }
      }
      catch (ApplicationException ex)
      {
        BTree = null;
      }
      catch (Exception ex)
      {
        BTree = null;
      }
    }

        private void button1_Click(object sender, EventArgs e)
        {
            BTree = null;
            Helper.Assert(true);
            ShowTreeView();
        }
    }
}
