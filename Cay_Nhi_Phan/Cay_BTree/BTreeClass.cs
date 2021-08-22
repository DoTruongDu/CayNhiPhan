using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Cay_BTree
{
  class BTreeClass
  {
    internal readonly int Order, HalfFull;
    internal int NbKeys = 0;
    internal int Deep;
    internal bool IsFull(BTreeNode node) => node.NbKeys >= Order;
    internal bool IsAtLeastHalfFull(BTreeNode node) => node.NbKeys >= HalfFull;
    internal bool IsMoreThanHalfFull(BTreeNode node) => node.NbKeys > HalfFull;

    internal BTreeNode Root;
    internal BTreeClass(int order)
    {
      Order = order;
      HalfFull = (order - 1) / 2;
      Root = new BTreeNode(order);
      Stack = new StackList();
      Stack.Add(new StackItem());
    }

    private class StackItem
    {
      internal BTreeNode Node;
      internal int Idx;

      internal void Set(BTreeNode node, int idxK)
      {
        Node = node;
        Idx = idxK;
      }

      internal void Get(out BTreeNode node, out int idxK)
      {
        node = Node;
        idxK = Idx;
      }
    }
    private class StackList : List<StackItem>
    {
    }
    private StackList Stack;
    public bool Add(int key)
    {
      if (Root.NbKeys == 0)
      {
        Root.AddKey(key);
        NbKeys++;
        return true;
      }

      BTreeNode node = Root;
      int idxProfondeur = 0;
      int idxK;
      for (; ; )
      {
        if (node.FindValue(key, out idxK))
        {
          return false; 
        }
        Stack[idxProfondeur].Set(node, idxK);
        if (node.Leaf)
        {
          break;
        }
        node = node.Child(idxK + 1);
        idxProfondeur++;
      }

      BTreeNode child = null;
      for (; idxProfondeur >= 0; idxProfondeur--)
      {
        Stack[idxProfondeur].Get(out node, out idxK);
        if (!node.IsFull)
        {
          node.Insert(idxK + 1, key, child);
          break;
        }
        node.Split(idxK + 1, ref key, ref child);
        if (node == Root)
        {
          Root = new BTreeNode(Order);
          Root.Init(key, node, child);
          Stack.Add(new StackItem());
          Deep++;
        }
      }
      NbKeys++;
      return true;
    }

    public bool Remove(int value)
    {
      int idxProfondeur = 0;
      BTreeNode node = Root;
      int idx;
      for (; ; )
      {
        bool exists = node.FindValue(value, out idx);
        if (exists)
        {
          break;
        }
        if (node.Leaf)
        {
          return false;
        }
        Stack[idxProfondeur++].Set(node, idx);
        node = node.Child(idx + 1);
      }

      if (!node.Leaf)
      {
        BTreeNode nodeMax = node.Child(idx);
        Stack[idxProfondeur++].Set(node, idx - 1);
        int idxMax;
        while (!nodeMax.Leaf)
        {
          idxMax = nodeMax.NbKeys - 1;
          Stack[idxProfondeur++].Set(nodeMax, idxMax);
          nodeMax = nodeMax.Child(idxMax + 1);
        }
        idxMax = nodeMax.NbKeys - 1;
        int valueMax = nodeMax.Key(idxMax);
        node.SetKey(idx, valueMax);
        node = nodeMax;
        idx = idxMax;
      }
      node.RemoveKey(idx);
      NbKeys--;

      if (!IsAtLeastHalfFull(node) && node != Root)
      {
        BalanceOuFusionne(node, idxProfondeur - 1);
      }
      return true;
    }

    private void BalanceOuFusionne(BTreeNode node, int idxProfondeur)
    {
      Stack[idxProfondeur].Get(out BTreeNode parent, out int idx);
      BTreeNode sibling;
      if (idx >= 0)
      {
        sibling = parent.Child(idx);
        if (IsMoreThanHalfFull(sibling))
        {
          if (!node.Leaf)
          {
            node.InsertChild(0, sibling.Child(sibling.NbChildren - 1));
            sibling.RemoveChild(sibling.NbChildren - 1);
          }
          node.InsertKey(0, parent.Key(idx));
          parent.SetKey(idx, sibling.Key(sibling.NbKeys - 1));
          sibling.RemoveKey(sibling.NbKeys - 1);
          return;
        }
      }
      if (idx < parent.NbKeys - 1)
      {
        sibling = parent.Child(idx + 2);
        if (IsMoreThanHalfFull(sibling))
        {
          if (!node.Leaf)
          {
            node.AddChild(sibling.Child(0));
            sibling.RemoveChild(0);
          }
          node.AddKey(parent.Key(idx + 1));
          parent.SetKey(idx + 1, sibling.Key(0));
          sibling.RemoveKey(0);
          return; 
        }
      }
      BTreeNode gauche, droite;
      if (idx >= 0)
      {
        gauche = parent.Child(idx);
        droite = node;
      }
      else
      {
        gauche = node;
        idx += 1;
        droite = parent.Child(idx + 1);
      }
      parent.RemoveChild(idx + 1);
      gauche.AddChildren(droite);
      gauche.AddKey(parent.Key(idx));
      parent.RemoveKey(idx);
      gauche.AddKeys(droite);
      if (!IsAtLeastHalfFull(parent) && parent != Root)
      {
        BalanceOuFusionne(parent, idxProfondeur - 1);
      }
      if (parent == Root && Root.NbKeys == 0)
      {
        Root = gauche;
        Stack.RemoveAt(Stack.Count - 1);
        Deep--;
      }

    }

    private class ScanDataClass
    {
      public bool previousValueSet = false;
      public int previousValue = int.MinValue;
      public bool bLgCheminSet = false;
      public int lgChemin = int.MinValue;
      public int nbKeysMax = 0;
      public int nbKeysActual = 0;
    }
    internal void Scan()
    {
      if (Root.NbKeys == 0)
      {
        return;
      }
      ScanDataClass scanData = new ScanDataClass();
      ScanBTree(Root, 0, scanData);
      Helper.Trace($"lgChemin={scanData.lgChemin}, nbkeysmax={scanData.nbKeysMax}, nbkeysactual={scanData.nbKeysActual}, remplissage={100.0 * scanData.nbKeysActual / scanData.nbKeysMax} %");
      Helper.Assert(scanData.nbKeysActual == NbKeys);
    }
    private void ScanBTree(BTreeNode node, int profondeur, ScanDataClass scanData)
    {
      scanData.nbKeysMax += Order - 1;
      scanData.nbKeysActual += node.NbKeys;
      bool bchildren = !node.Leaf;
      Helper.Assert(node == Root || IsAtLeastHalfFull(node));
      if (bchildren)
      {
        // On descend en bas à gauche
        Helper.Assert(node.NbKeys == node.NbChildren - 1);
        ScanBTree(node.Child(0), profondeur + 1, scanData);
      }
      else if (scanData.bLgCheminSet)
      {
        Helper.Assert(profondeur == scanData.lgChemin);
      }
      else
      {
        scanData.lgChemin = profondeur;
        scanData.bLgCheminSet = true;
      }
      for (int i = 0; i < node.NbKeys; i++)
      {
        int value = node.Key(i);
        if (!scanData.previousValueSet)
        {
          Helper.Assert(i == 0 && !bchildren);
          scanData.previousValue = node.Key(0);
          scanData.previousValueSet = true;
        }
        else
        {
          // previousValue est censée être la plus grande key dans le sous-arbre à gauche 
          Helper.Assert(scanData.previousValue < value);
          scanData.previousValue = value;
        }
        if (bchildren)
        {
          ScanBTree(node.Child(i + 1), profondeur + 1, scanData);
        }
      }
    }

  }
}
