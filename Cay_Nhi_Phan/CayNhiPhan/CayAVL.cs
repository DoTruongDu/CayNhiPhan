using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
namespace Cay_Nhi_Phan
{

	public partial class AVLTree_Form : Form
	{

		const int EQUAL = 0;        // Can bang
		const int LEFT = -1;        // Trai
		const int RIGHT = 1;        // Phai

		int Total_Node = 0;     // tong so node
		int Total_Leaf_Node = 0;    // tong so leaf node
		int Total_Intermediate_Node = 0;     // tong so node trung gian
		int The_Height_Tree = 0;         // chieu cao cay

		public class_node Root;
		public class_node Node_Temp; // node dung de dung tam khi bat su kien click Delete_ContextMenuStrip
		int Speed;
		Bitmap bitmap;
		Graphics g;
		RichTextBox Info_RichTextBox = new RichTextBox();
		RichTextBox Compare_RichTextBox = new RichTextBox();
		List<int> Way = new List<int>();
		List<int> RandomList = new List<int>();

		int treeHeight = 0;
		bool isBreak = false;

		public AVLTree_Form()
		{
			InitializeComponent();
			bitmap = new Bitmap(Main_PictureBox.Width, Main_PictureBox.Height);
			g = Graphics.FromImage(bitmap);
			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
		}

		#region Do Hoa
		public void VeCanh(PointF a, PointF b)
		{
			g.DrawLine(new Pen(Color.GreenYellow, 2), a.X + 16, a.Y + 32, b.X + 16, b.Y);
			Main_PictureBox.Image = bitmap;
		}
		public void DrawNode(class_node A)
		{
			g.DrawImage(Cay_Nhi_Phan.Properties.Resources.ellip_blue, A.vitri.X, A.vitri.Y);
			Pen pen = new Pen(Color.Black, 3);
			g.DrawEllipse(pen, A.vitri.X, A.vitri.Y, 30, 30);
			if (A.number < 10)
			{
				if (A.number >= 0)
				{
					g.DrawString(A.number.ToString(), new Font(FontFamily.GenericSerif, 15, FontStyle.Bold), new SolidBrush(Color.White), new PointF(A.vitri.X + 8, A.vitri.Y + 4f));
				}
				else
				{
					if (A.number > -100)
					{
						if (A.number > -10)
						{
							g.DrawString(A.number.ToString(), new Font(FontFamily.GenericSerif, 15, FontStyle.Bold), new SolidBrush(Color.White), new PointF(A.vitri.X + 4, A.vitri.Y + 4f));
						}
						else
						{
							g.DrawString(A.number.ToString(), new Font(FontFamily.GenericSerif, 15, FontStyle.Bold), new SolidBrush(Color.White), new PointF(A.vitri.X + -1, A.vitri.Y + 4f));
						}
					}
					else
					{
						g.DrawString(A.number.ToString(), new Font(FontFamily.GenericSerif, 15, FontStyle.Bold), new SolidBrush(Color.White), new PointF(A.vitri.X - 6, A.vitri.Y + 4f));
					}
				}
			}
			else
			{
				if (A.number >= 100)
				{
					g.DrawString(A.number.ToString(), new Font(FontFamily.GenericSerif, 15, FontStyle.Bold), new SolidBrush(Color.White), new PointF(A.vitri.X - 1, A.vitri.Y + 4f));
				}
				else
				{
					g.DrawString(A.number.ToString(), new Font(FontFamily.GenericSerif, 15, FontStyle.Bold), new SolidBrush(Color.White), new PointF(A.vitri.X + 4, A.vitri.Y + 4f));
				}
			}
			Main_PictureBox.Image = bitmap;
		}
		public void DrawNodeRed(class_node A)
		{
			g.DrawImage(Cay_Nhi_Phan.Properties.Resources.ellip_red, A.vitri.X, A.vitri.Y);
			if (A.number < 10)
			{
				if (A.number >= 0)
				{
					g.DrawString(A.number.ToString(), new Font(FontFamily.GenericSerif, 15, FontStyle.Bold), new SolidBrush(Color.White), new PointF(A.vitri.X + 8, A.vitri.Y + 4f));
				}
				else
				{
					if (A.number > -100)
					{
						if (A.number > -10)
						{
							g.DrawString(A.number.ToString(), new Font(FontFamily.GenericSerif, 15, FontStyle.Bold), new SolidBrush(Color.White), new PointF(A.vitri.X + 4, A.vitri.Y + 4f));
						}
						else
						{
							g.DrawString(A.number.ToString(), new Font(FontFamily.GenericSerif, 15, FontStyle.Bold), new SolidBrush(Color.White), new PointF(A.vitri.X + -1, A.vitri.Y + 4f));
						}
					}
					else
					{
						g.DrawString(A.number.ToString(), new Font(FontFamily.GenericSerif, 15, FontStyle.Bold), new SolidBrush(Color.White), new PointF(A.vitri.X - 6, A.vitri.Y + 4f));
					}
				}
			}
			else
			{
				if (A.number >= 100)
				{
					g.DrawString(A.number.ToString(), new Font(FontFamily.GenericSerif, 15, FontStyle.Bold), new SolidBrush(Color.White), new PointF(A.vitri.X - 1, A.vitri.Y + 4f));
				}
				else
				{
					g.DrawString(A.number.ToString(), new Font(FontFamily.GenericSerif, 15, FontStyle.Bold), new SolidBrush(Color.White), new PointF(A.vitri.X + 4, A.vitri.Y + 4f));
				}
			}
			Main_PictureBox.Image = bitmap;
		}
		public void DrawSearch(class_node A)
		{
			g.DrawImage(Cay_Nhi_Phan.Properties.Resources.search, A.vitri.X - 5, A.vitri.Y);
			Main_PictureBox.Image = bitmap;
		}
		public void DrawDelete(class_node A)
		{
			g.DrawImage(Cay_Nhi_Phan.Properties.Resources.delete, A.vitri.X, A.vitri.Y);
			Main_PictureBox.Image = bitmap;
		}
		public void DiChuyen(ref class_node A, PointF B, int cv)
		{
			A.locationOld = A.vitri;
			XacDinhTocDo();
			float a = (B.Y - A.vitri.Y) / (B.X - A.vitri.X);
			float b = B.Y - a * B.X;
			float deltaX = Math.Abs(B.X - A.locationOld.X);
			if (A.locationOld.X - B.X < 0)
			{
				//while (A.vitri.X - B.X < 0)			//A.vitri.X - B.X < 0
				for (int i = 0; i < Speed; i++)
				{
					g.Clear(Color.White);
					A.vitri.X += deltaX / Speed;
					A.vitri.Y = a * A.vitri.X + b;
					if (cv == 1)
					{
						VeCay_special(Root, A);
						DrawNodeRed(A);
					}
					else
					{
						if (cv == 2)
						{
							VeCay_normal(Root);
							DrawSearch(A);
						}
					}
					Thread.Sleep(1);
					Application.DoEvents();
				}
			}
			else
			{
				//while (A.vitri.X - B.X > 0)
				for (int i = 0; i < Speed; i++)
				{
					g.Clear(Color.White);
					A.vitri.X -= (deltaX / Speed);
					A.vitri.Y = a * A.vitri.X + b;
					if (cv == 1)
					{
						VeCay_special(Root, A);
						DrawNodeRed(A);
					}
					else
					{
						if (cv == 2)
						{
							VeCay_normal(Root);
							DrawSearch(A);
						}
					}
					Thread.Sleep(1);
					Application.DoEvents();
				}
			}
		}



		// Di chuyển node xuống vị trí pLeft hoặc pRight
		public void MoveDown(ref class_node n, int heso, int cv)
		{
			if (heso == LEFT) //move_down_left
			{
				DiChuyen(ref n, nodeLR(n.vitri, LEFT), cv);
			}
			if (heso == RIGHT) //move_down_right
			{
				DiChuyen(ref n, nodeLR(n.vitri, RIGHT), cv);
			}
		}
		// Hàm nhận vào 1 node và trả về vị trí node pPeft hoặc pRight của node đó
		public PointF nodeLR(PointF l, int traiphai)
		{
			PointF kq = new PointF();
			int vt_y = ((Convert.ToInt32(l.Y - 20)) + 70) / 70;
			int partwidth = 1024 / (int)Math.Pow(2, (vt_y + 1));
			if (traiphai == -1)  //tra ve vi tri node con ben trai
			{
				kq.X = l.X - partwidth;
				kq.Y = l.Y + 70;
				return kq;
			}
			else  //tra ve vi tri node con ben phai 
			{
				kq.X = l.X + partwidth;
				kq.Y = l.Y + 70;
				return kq;
			}
		}
		// Vẽ cây bình thường
		public void VeCay_normal(class_node n)
		{
			if (Root != null)
				DrawNode(Root);
			if (n != null)
			{
				if (n.left != null)
				{
					DrawNode(n.left);
					VeCanh(n.vitri, n.left.vitri);
				}
				if (n.right != null)
				{

					DrawNode(n.right);
					VeCanh(n.vitri, n.right.vitri);
				}
				VeCay_normal(n.left);
				VeCay_normal(n.right);
			}
		}
		public void VeCay_special(class_node n, class_node a)
		{
			if (Root != null)
				DrawNode(Root);
			if (n != null)
			{
				if (n.left != null && n.left != a)
				{
					DrawNode(n.left);
					VeCanh(n.vitri, n.left.vitri);
				}
				if (n.right != null && n.right != a)
				{
					DrawNode(n.right);
					VeCanh(n.vitri, n.right.vitri);
				}
				VeCay_special(n.left, a);
				VeCay_special(n.right, a);
			}
		}
		// Xác dịnh lại vị trí cho node để vẽ cây
		public void Xd_ViTri(ref class_node n)
		{
			PointF vt = new PointF();
			if (Root != null)
			{
				vt = new PointF(512, 20);
				Root.vitri = vt;
			}
			if (n != null)
			{
				if (n.left != null)
				{
					vt = nodeLR(n.vitri, -1);
					n.left.vitri = vt;
				}
				if (n.right != null)
				{
					vt = nodeLR(n.vitri, 1);
					n.right.vitri = vt;
				}
				Xd_ViTri(ref n.left);
				Xd_ViTri(ref n.right);
			}
		}
		public void Xd_ViTriCu(ref class_node n)
		{
			if (Root != null)
			{
				Root.locationOld = Root.vitri;
			}
			if (n != null)
			{
				if (n.left != null)
				{
					n.left.locationOld = n.left.vitri;
				}
				if (n.right != null)
				{
					n.right.locationOld = n.right.vitri;
				}
				Xd_ViTriCu(ref n.left);
				Xd_ViTriCu(ref n.right);
			}
		}

		public void Xd_ViTriMoi(ref class_node n)
		{
			PointF vt = new PointF();
			if (Root != null)
			{
				vt = new PointF(512, 20);
				Root.locationNew = vt;
			}
			if (n != null)
			{
				if (n.left != null)
				{
					vt = nodeLR(n.locationNew, -1);
					n.left.locationNew = vt;
				}
				if (n.right != null)
				{
					vt = nodeLR(n.locationNew, 1);
					n.right.locationNew = vt;
				}
				Xd_ViTriMoi(ref n.left);
				Xd_ViTriMoi(ref n.right);
			}
		}

		public void DiChuyenCay(ref class_node node)
		{
			if (node != null)
			{
				if (node.locationOld.X != node.locationNew.X)
				{
					DiChuyenSpecial(ref node, node.locationNew);
				}
				DiChuyenCay(ref node.left);
				DiChuyenCay(ref node.right);
			}
		}

		public void DiChuyenSpecial(ref class_node A, PointF B)
		{
			XacDinhTocDo();
			float a = (B.Y - A.locationOld.Y) / (B.X - A.locationOld.X);
			float b = B.Y - a * B.X;
			float deltaX = Math.Abs(B.X - A.locationOld.X);

			if (A.vitri.X - B.X < 0) //A.vitri.X - B.X < 0
			{
				A.vitri.X += (deltaX / Speed);
				A.vitri.Y = a * A.vitri.X + b;
			}
			else
			{
				A.vitri.X -= (deltaX / Speed);
				A.vitri.Y = a * A.vitri.X + b;
			}
		}
		private void XacDinhTocDo()
		{
			int Temp = (High(Root));
			if (Speed_ComboBox.Text != "1" && Speed_ComboBox.Text != "2" && Speed_ComboBox.Text != "3" && Speed_ComboBox.Text != "4")
			{
				Speed_ComboBox.Text = "2";
			}
			Speed = (5 - Convert.ToInt32(Speed_ComboBox.Text)) * (int)(10);
		}
		
		private void ShowTextBox(class_node node)
		{
			Info_RichTextBox.Clear();
			if ((Convert.ToInt32(node.vitri.X + 200) >= Main_PictureBox.Width) || (Convert.ToInt32(node.vitri.Y + 150) >= Main_PictureBox.Height))
			{
				Info_RichTextBox.Location = new Point(Convert.ToInt32(node.vitri.X - 50), Convert.ToInt32(node.vitri.Y - 150));
			}
			else
			{
				
					Info_RichTextBox.Location = new Point(Convert.ToInt32(node.vitri.X + 50), Convert.ToInt32(node.vitri.Y + 50));
			}
			Info_RichTextBox.Size = new Size(180, 130);
			Info_RichTextBox.AppendText(" Node " + node.number);
			if (node == Root)
			{
				Info_RichTextBox.AppendText(" -  Node goc ");
			}
			else
			{
				if (node.left == null && node.right == null)
				{
					Info_RichTextBox.AppendText(" -  Node la ");
				}
				else
				{
					Info_RichTextBox.AppendText(" -  Node trung gian ");
				}
			}
			int dem = 0;
			XacDinhMuc1Node(ref Root,ref node,ref dem);
			Info_RichTextBox.AppendText("\n- Muc cua Node: "+dem);
			Info_RichTextBox.AppendText("\n- He so can bang: ");
			switch (node.canbang)
			{
				case LEFT: Info_RichTextBox.AppendText("LH"); break;
				case EQUAL: Info_RichTextBox.AppendText("EH"); break;
				case RIGHT: Info_RichTextBox.AppendText("RH"); break;
			}
			if (node.left != null)
				Info_RichTextBox.AppendText("\n- Node.Left: " + node.left.number);
			else
				Info_RichTextBox.AppendText("\n- Node.Left rong");
			if (node.right != null)
				Info_RichTextBox.AppendText("\n- Node.Right: " + node.right.number);
			else
				Info_RichTextBox.AppendText("\n- Node.Right rong");

			Info_RichTextBox.AppendText("\n <RightClick> to delete that node");
			Info_RichTextBox.AppendText("\n\n Move out the node and press <Esc> key to hide textbox");
			Main_PictureBox.Controls.Add(Info_RichTextBox);
		}
		#endregion

		#region Build Tree 
		void Rotate_Left_Left(ref class_node node)
		{
			class_node p;
			p = node.left;
			node.left = p.right;
			p.right = node;
			switch (p.canbang)
			{
				case LEFT:
					node.canbang = EQUAL;
					p.canbang = EQUAL;
					break;
				case EQUAL:
					p.canbang = RIGHT;
					node.canbang = LEFT;
					break;
			}
			node = p;
		}

		//cay con phai lech phai
		void Rotate_Right_Right(ref class_node node)
		{
			class_node p;
			p = node.right;
			node.right = p.left;
			p.left = node;
			switch (p.canbang)
			{
				case EQUAL:
					node.canbang = RIGHT;
					//p.canbang = EQUAL;
					p.canbang = LEFT;
					break;
				case RIGHT:
					p.canbang = EQUAL;
					node.canbang = EQUAL;
					break;
			}
			node = p;
		}

		//cay con phai lech trai
		void Rotate_Right_Left(ref class_node node)
		{
			class_node p1, p2;
			p1 = node.right;
			p2 = p1.left;
			node.right = p2.left;
			p1.left = p2.right;
			p2.left = node;
			p2.right = p1;
			switch (p2.canbang)
			{
				case LEFT:
					node.canbang = EQUAL;
					p1.canbang = RIGHT;
					break;
				case EQUAL:
					node.canbang = EQUAL;
					p1.canbang = EQUAL;
					break;
				case RIGHT:
					node.canbang = LEFT;
					p1.canbang = EQUAL;
					break;
			}
			p2.canbang = EQUAL;
			node = p2;
		}

		//cay con trai lech phai
		void Rotate_Left_Right(ref class_node node)
		{
			class_node p1, p2;
			p1 = node.left;
			p2 = p1.right;
			node.left = p2.right;
			p1.right = p2.left;
			p2.right = node;
			p2.left = p1;

			switch (p2.canbang)
			{
				case LEFT:
					node.canbang = RIGHT;
					p1.canbang = EQUAL;
					break;
				case EQUAL:
					node.canbang = EQUAL;
					p1.canbang = EQUAL;
					break;
				case RIGHT:
					node.canbang = EQUAL;
					p1.canbang = LEFT;
					break;
			}
			p2.canbang = EQUAL;
			node = p2;
		}

		//Can bang khi cay lech trai
		int BalanceLeft(ref class_node node)
		{
			class_node p;
			p = node.left;
			switch (p.canbang)
			{
				case LEFT:
					Rotate_Left_Left(ref node);
					return 2;
				case EQUAL:
					Rotate_Left_Left(ref node);
					return 1;
				case RIGHT:
					Rotate_Left_Right(ref node);
					return 2;
			}
			return 0;
		}

		//can bang cay lech phai
		int BalanceRight(ref class_node node)
		{
			class_node p;
			p = node.right;
			switch (p.canbang)
			{
				case LEFT:
					Rotate_Right_Left(ref node);
					return 2;
				case EQUAL:
					Rotate_Right_Right(ref node);
					return 1;
				case RIGHT:
					Rotate_Right_Right(ref node);
					return 2;
			}
			return 0;
		}
		private int InsertNode(ref class_node node, int number)
		{

			int Res;
			if (node == null)
			{
				node = new class_node(number);
				if (Way.Count != 0)
				{
					class_node Temp = Root;
					DrawNodeRed(Temp);
					for (int i = 0; i < Way.Count; i++)
					{
						if (Compare_CheckBox.Checked == true)
						{
							Compare_RichTextBox.Clear();
							Compare_RichTextBox.Location = new Point(Convert.ToInt32(node.vitri.X + 50), Convert.ToInt32(node.vitri.Y + 25));
							Compare_RichTextBox.Size = new Size(50, 25);
							if (Temp.number == node.number)
							{
								return 2;
							}
							if (Way[i] == 1)
							{
								Compare_RichTextBox.AppendText(node.number + " > " + Temp.number);
								Temp = Temp.right;
							}
							else
							{
								Compare_RichTextBox.AppendText(node.number + " < " + Temp.number);
								Temp = Temp.left;
							}
							Main_PictureBox.Controls.Add(Compare_RichTextBox);
							Application.DoEvents();
							Thread.Sleep(1200);
							Main_PictureBox.Controls.Remove(Compare_RichTextBox);
						}
						else
						{
							if (Way[i] == 1)
							{
								Temp = Temp.right;
							}
							else
							{
								if (Way[i] == -1)
								{
									Temp = Temp.left;
								}
							}
						}
						MoveDown(ref node, Way[i], 1);
					}
				}
			}
			else
			{
				if (node.number == number)
				{
					return 0;
				}
				if (number < node.number)
				{
					Way.Add(LEFT);
					Res = InsertNode(ref node.left, number);
					if (Res < 2) return Res;

					//Res >= 2
					switch (node.canbang)
					{
						case RIGHT:
							node.canbang = EQUAL;
							return 1;
						case EQUAL:
							node.canbang = LEFT;
							return 2;
						case LEFT:
							BalanceLeft(ref node);
							return 1;
					}
				}
				else
				{
					Way.Add(RIGHT);
					Res = InsertNode(ref node.right, number);
					if (Res < 2) return Res;

					//Res >= 2
					switch (node.canbang)
					{
						case LEFT:
							node.canbang = EQUAL;
							return 1;
						case EQUAL:
							node.canbang = RIGHT;
							return 2;
						case RIGHT:
							BalanceRight(ref node);
							return 1;
					}

				}
			}
			return 2;
		}

		private int DelNode(ref class_node node, int number)
		{
			int Res;
			//Khong ton tai node nay tren cay
			if (node == null)
			{
				class_node Temp_Run = new class_node();
				if (Way.Count != 0)
				{
					class_node Temp=Root;
					DrawSearch(Temp);
					for (int i = 0; i < Way.Count - 1; i++)
					{
						if (Compare_CheckBox.Checked == true)
						{
							Compare_RichTextBox.Clear();
							Compare_RichTextBox.Location = new Point(Convert.ToInt32(Temp_Run.vitri.X + 50), Convert.ToInt32(Temp_Run.vitri.Y + 25));
							Compare_RichTextBox.Size = new Size(50, 25);
							if (Temp.number == node.number)
							{
								continue;
							}
							if (Way[i] == 1)
							{
								Compare_RichTextBox.AppendText(node.number + " > " + Temp.number);
								Temp = Temp.right;
							}
							else
							{
								Compare_RichTextBox.AppendText(node.number + " < " + Temp.number);
								Temp = Temp.left;
							}
							Main_PictureBox.Controls.Add(Compare_RichTextBox);
							Application.DoEvents();
							Thread.Sleep(1200);
							Main_PictureBox.Controls.Remove(Compare_RichTextBox);
						}
						else
						{
							if (Way[i] == 1)
							{
								Temp = Temp.right;
							}
							else
							{
								if (Way[i] == -1)
								{
									Temp = Temp.left;
								}
							}
						}
						MoveDown(ref Temp_Run, Way[i], 2);
					}
				}
				MessageBox.Show(" Khong tim thay node co gia tri can xoa ");
				return 0;
			}

			if (node.number == number)
			{
				class_node Temp_Run = new class_node();
				if (Way.Count != 0)
				{
					class_node Temp2 = Root;
					DrawSearch(Temp2);
					for (int i = 0; i < Way.Count; i++)
					{
						if (Compare_CheckBox.Checked == true)
						{
							Compare_RichTextBox.Clear();
							Compare_RichTextBox.Location = new Point(Convert.ToInt32(Temp_Run.vitri.X + 50), Convert.ToInt32(Temp_Run.vitri.Y + 25));
							Compare_RichTextBox.Size = new Size(50, 25);
							if (Temp2.number == node.number)
							{
								continue;
							}
							if (Way[i] == 1)
							{
								Compare_RichTextBox.AppendText(node.number + " > " + Temp2.number);
								Temp2 = Temp2.right;
							}
							else
							{
								Compare_RichTextBox.AppendText(node.number + " < " + Temp2.number);
								Temp2 = Temp2.left;
							}
							Main_PictureBox.Controls.Add(Compare_RichTextBox);
							Application.DoEvents();
							Thread.Sleep(1200);
							Main_PictureBox.Controls.Remove(Compare_RichTextBox);
						}
						else
						{
							if (Way[i] == 1)
							{
								Temp2 = Temp2.right;
							}
							else
							{
								if (Way[i] == -1)
								{
									Temp2 = Temp2.left;
								}
							}
						}
						MoveDown(ref Temp_Run, Way[i], 2);
					}
				}
				g.Clear(Color.White);
				VeCay_normal(Root);
				DrawDelete(Temp_Run);

				MessageBox.Show(" Da tim thay node co gia tri " + number + " va se xoa ngay !");

				//Root->info = x
				class_node Temp = node;


				if (node.left == null)
				{
					node = node.right;
					Res = 2;
				}
				else
				{
					if (node.right == null)
					{
						node = node.left;
						Res = 2;
					}
					else
					{
						Res = SearchStandFor(ref Temp, ref node.right);
						if (Res < 2) return Res;
						switch (node.canbang)
						{
							case RIGHT:
								node.canbang = EQUAL;
								return 2;
							case EQUAL:
								node.canbang = LEFT;
								return 1;
							case LEFT:
								return BalanceLeft(ref node);
						}
					}
					Temp = null;
					return Res;
				}
			}
			else
			{
				//Root->info > x => Sang ben trai tim xoa
				if (node.number > number)
				{
					Way.Add(LEFT);
					Res = DelNode(ref node.left, number);
					if (Res < 2) return Res;

					//Chieu cao bi thay doi
					switch (node.canbang)
					{
						case LEFT:
							node.canbang = EQUAL;
							return 2;
						case EQUAL:
							node.canbang = RIGHT;
							return 1;
						case RIGHT:
							return BalanceRight(ref node);
					}
				}

				if (node.number < number)
				{
					Way.Add(RIGHT);
					Res = DelNode(ref node.right, number);

					if (Res < 2) return Res;

					switch (node.canbang)
					{
						case LEFT:
							return BalanceLeft(ref node);
						case EQUAL:
							node.canbang = LEFT;
							return 1;
						case RIGHT:
							node.canbang = EQUAL;
							return 2;
					}
				}
			}
			return 2;
		}
		private int DelNode_Special(ref class_node node, int number)
		{
			int Res;
			
		
			if (node.number == number)
			{
				g.Clear(Color.White);
				VeCay_normal(Root);
				DrawDelete(node);
				MessageBox.Show("Xoa thanh cong");

				//Root->info = x
				class_node Temp = node;


				if (node.left == null)
				{
					node = node.right;
					Res = 2;
				}
				else
				{
					if (node.right == null)
					{
						node = node.left;
						Res = 2;
					}
					else
					{
						Res = SearchStandFor(ref Temp, ref node.right);
						if (Res < 2) return Res;
						switch (node.canbang)
						{
							case RIGHT:
								node.canbang = EQUAL;
								return 2;
							case EQUAL:
								node.canbang = LEFT;
								return 1;
							case LEFT:
								return BalanceLeft(ref node);
						}
					}
					Temp = null;
					return Res;
				}
			}
			else
			{
				//Root->info > x => Sang ben trai tim xoa
				if (node.number > number)
				{
					
					Res = DelNode_Special(ref node.left, number);
					if (Res < 2) return Res;

					//Chieu cao bi thay doi
					switch (node.canbang)
					{
						case LEFT:
							node.canbang = EQUAL;
							return 2;
						case EQUAL:
							node.canbang = RIGHT;
							return 1;
						case RIGHT:
							return BalanceRight(ref node);
					}
				}

				if (node.number < number)
				{
					Res = DelNode_Special(ref node.right, number);

					if (Res < 2) return Res;

					switch (node.canbang)
					{
						case LEFT:
							return BalanceLeft(ref node);
						case EQUAL:
							node.canbang = LEFT;
							return 1;
						case RIGHT:
							node.canbang = EQUAL;
							return 2;
					}
				}
			}
			return 2;
		}
		//Tim node the mang
		private int SearchStandFor(ref class_node Temp, ref class_node node)
		{
			int Res;

			if (node.left != null)
			{
				Res = SearchStandFor(ref Temp, ref node.left);

				if (Res < 2) return Res;

				switch (node.canbang)
				{
					case LEFT:
						node.canbang = EQUAL;
						return 2;
					case EQUAL:
						node.canbang = RIGHT;
						return 1;
					case RIGHT:
						return BalanceRight(ref node);
				}
			}
			else
			{
				Temp.number = node.number;
				Temp = node;
				node = node.right;
				return 2;
			}
			return 0;
		}



		private void FindANode(class_node node, int number)
		{
			if (node == null)
			{
				class_node Node_Run = new class_node();
				if (Way.Count != 0)
				{
					class_node Temp = Root;
					DrawSearch(Temp);
					for (int i = 0; i < Way.Count - 1; i++)
					{
						if (Compare_CheckBox.Checked == true)
						{
							Compare_RichTextBox.Clear();
							Compare_RichTextBox.Location = new Point(Convert.ToInt32(Node_Run.vitri.X + 50), Convert.ToInt32(Node_Run.vitri.Y + 25));
							Compare_RichTextBox.Size = new Size(50, 25);
							if (Temp.number == node.number)
							{
								continue;
							}
							if (Way[i] == 1)
							{
								Compare_RichTextBox.AppendText(node.number + " > " + Temp.number);
								Temp = Temp.right;
							}
							else
							{
								Compare_RichTextBox.AppendText(node.number + " < " + Temp.number);
								Temp = Temp.left;
							}
							Main_PictureBox.Controls.Add(Compare_RichTextBox);
							Application.DoEvents();
							Thread.Sleep(1200);
							Main_PictureBox.Controls.Remove(Compare_RichTextBox);
						}
						else
						{
							if (Way[i] == 1)
							{
								Temp = Temp.right;
							}
							else
							{
								if (Way[i] == -1)
								{
									Temp = Temp.left;
								}
							}
						}
						MoveDown(ref Node_Run, Way[i], 2);
					}
				}
				MessageBox.Show(" Khong tim thay node nao co gia tri " + number + " !");
				Node_Run = null;
				g.Clear(Color.White);
				VeCay_normal(Root);
				Input_TextBox.Clear();
				return;
			}
			else
			{
				if (node == Root && node.number == number)
				{
					DrawSearch(Root);
					MessageBox.Show(" Da tim thay node co gia tri " + number + " !");
					g.Clear(Color.White);
					VeCay_normal(Root);
					Input_TextBox.Clear();
					return;
				}
				if (node.number == number)
				{
					class_node Node_Run = new class_node();
					if (Way.Count != 0)
					{
						class_node Temp = Root;
						DrawSearch(Temp);
						for (int i = 0; i < Way.Count; i++)
						{
							if (Compare_CheckBox.Checked == true)
							{
								Compare_RichTextBox.Clear();
								Compare_RichTextBox.Location = new Point(Convert.ToInt32(Node_Run.vitri.X + 50), Convert.ToInt32(Node_Run.vitri.Y + 25));
								Compare_RichTextBox.Size = new Size(50, 25);
								if (Temp.number == node.number)
								{
									continue;
								}
								if (Way[i] == 1)
								{
									Compare_RichTextBox.AppendText(node.number + " > " + Temp.number);
									Temp = Temp.right;
								}
								else
								{
									Compare_RichTextBox.AppendText(node.number + " < " + Temp.number);
									Temp = Temp.left;
								}
								Main_PictureBox.Controls.Add(Compare_RichTextBox);
								Application.DoEvents();
								Thread.Sleep(1200);
								Main_PictureBox.Controls.Remove(Compare_RichTextBox);
							}
							else
							{
								if (Way[i] == 1)
								{
									Temp = Temp.right;
								}
								else
								{
									if (Way[i] == -1)
									{
										Temp = Temp.left;
									}
								}
							}
							MoveDown(ref Node_Run, Way[i], 2);
						}
					}
					MessageBox.Show(" Da tim thay node co gia tri " + number + " !");
					Node_Run = null;
					g.Clear(Color.White);
					VeCay_normal(Root);
					Input_TextBox.Clear();
					return;
				}
				if (number < node.number)
				{
					Way.Add(LEFT);
					FindANode(node.left, number);
				}
				else
				{
					Way.Add(RIGHT);
					FindANode(node.right, number);
				}
			}
		}

		private void InItTree(ref class_node node)
		{
			node = null;
		}

		private int High(class_node node)
		{
			if (node == null)
			{
				return 0;
			}
			int a = High(node.left);
			int b = High(node.right);
			if (a > b)
			{
				return (a + 1);
			}
			return (b + 1);
		}
		private void TotalLeafNode(class_node node)
		{
			if (node != null)
			{
				if (node.left == null && node.right == null)
				{
					Total_Leaf_Node++;
				}
				TotalLeafNode(node.left);
				TotalLeafNode(node.right);
			}
		}
		private void TotalNode(class_node node)
		{
			if (node != null)
			{
				Total_Node++;
				TotalNode(node.left);
				TotalNode(node.right);
			}
		}
		private void XacDinhSoPhanTu()
		{
			Total_Node = 0;
			TotalNode(Root);
			Total_Node_TextBox.Text = Total_Node.ToString();
			Total_Leaf_Node = 0;
			TotalLeafNode(Root);
			Total_Leaf_Node_TextBox.Text = Total_Leaf_Node.ToString();
			if (Total_Node == 0 || Total_Node == 1)
			{
				Total_Intermediate_Node = 0;
				Total_Intermediate_Node_TextBox.Text = Total_Intermediate_Node.ToString();
			}
			else
			{
				Total_Intermediate_Node_TextBox.Text = (Total_Node - Total_Leaf_Node - 1).ToString();
			}
			The_Height_Tree = High(Root) - 1;
			if (The_Height_Tree == -1)
			{
				The_Height_Tree = 0;
			}
			The_Height_Tree_TextBox.Text = The_Height_Tree.ToString();
			treeHeight = The_Height_Tree;
		}
		public void XacDinhMuc1Node(ref class_node node, ref class_node nodecantim, ref int dem)
		{
			if (node.number == nodecantim.number)
			{
				return;
			}
			if (node.number > nodecantim.number)
			{
				dem++;
				XacDinhMuc1Node(ref node.left, ref nodecantim, ref dem);
			}
			else
			{
				dem++;
				XacDinhMuc1Node(ref node.right, ref nodecantim, ref dem);
			}
		}
		private void LRN(class_node node)
		{
			if (node != null)
			{
				LRN(node.left);
				LRN(node.right);
				g.Clear(Color.White);
				VeCay_normal(Root);
				DrawSearch(node);
				Thread.Sleep(1000);
				Application.DoEvents();
			}
		}
		private void NLR(class_node node)
		{
			if (node != null)
			{
				g.Clear(Color.White);
				VeCay_normal(Root);
				DrawSearch(node);
				Thread.Sleep(1000);
				Application.DoEvents();
				NLR(node.left);
				NLR(node.right);
			}
		}
		private void LNR(class_node node)
		{
			if (node != null)
			{
				LNR(node.left);
				g.Clear(Color.White);
				VeCay_normal(Root);
				DrawSearch(node);
				Thread.Sleep(1000);
				Application.DoEvents();
				LNR(node.right);
			}
		}
		#endregion

		#region Control 

		//  xoa toan cay 
		private void Del_Tree_Button_Click(object sender, EventArgs e)
		{
			treeHeight = 0;
			InItTree(ref Root);
			g.Clear(Color.White);
			Main_PictureBox.Image = bitmap;
			XacDinhSoPhanTu();
			Main_PictureBox.Controls.Remove(Info_RichTextBox);
			MessageBox.Show(" Da xoa thanh cong cay! ");
		}

		private void Find_Button_Click(object sender, EventArgs e)
		{
			if (Input_TextBox.Text.Length > 0)
			{
				try
				{
					int Temp = Convert.ToInt32(Input_TextBox.Text);
					Way.Clear();
					FindANode(Root, Temp);
					Input_TextBox.Clear();
				}
				catch
				{
					MessageBox.Show(" Gia tri nhap khong dung");
					Input_TextBox.Clear();
				}
			}
			else
			{
				MessageBox.Show(" Ban chua nhap gia tri ");
				Input_TextBox.Clear();
			}
		}

		private void Del_Node_Button_Click(object sender, EventArgs e)
		{
			if (Input_TextBox.Text.Length > 0)
			{
				try
				{
					int Temp = Convert.ToInt32(Input_TextBox.Text);
					Way.Clear();
					int StatusDelNode = DelNode(ref Root, Temp);
					if (StatusDelNode == 0)
					{
						g.Clear(Color.White);
						Xd_ViTri(ref Root);
						VeCay_normal(Root);
						return;
					}
					Xd_ViTriCu(ref Root);
					Xd_ViTriMoi(ref Root);
					XacDinhTocDo();
					for (int i = 0; i < Speed; i++)
					{
						DiChuyenCay(ref Root);
						g.Clear(Color.White);
						VeCay_normal(Root);
						Thread.Sleep(2);
						Application.DoEvents();
					}
					g.Clear(Color.White);
					Xd_ViTri(ref Root);
					VeCay_normal(Root);
					XacDinhSoPhanTu();
					Input_TextBox.Clear();
				}
				catch
				{
					MessageBox.Show(" Gia tri nhap khong dung");
					Input_TextBox.Clear();
				}
			}
			else
			{
				MessageBox.Show(" Ban chua nhap gia tri ");
				Input_TextBox.Clear();
			}
		}

		private void Input_TextBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter && Input_TextBox.Text.Length > 0)
			{
				int StatusInsert;
				try
				{
					int Temp = Convert.ToInt32(Input_TextBox.Text);
					if (Temp >= -99 && Temp <= 999)
					{
						Way.Clear();
						if (treeHeight <= 5) {
							StatusInsert = InsertNode(ref Root, Temp);
							if (StatusInsert == 0)
							{
								MessageBox.Show(" Da ton tai gia tri ");
								return;
							}
							else
							{
								Xd_ViTriCu(ref Root);
								Xd_ViTriMoi(ref Root);
								XacDinhTocDo();
								for (int i = 0; i < Speed; i++)
								{
									DiChuyenCay(ref Root);
									g.Clear(Color.White);
									VeCay_normal(Root);
									Thread.Sleep(2);
									Application.DoEvents();
								}
								g.Clear(Color.White);
								Xd_ViTri(ref Root);
								VeCay_normal(Root);
								XacDinhSoPhanTu();
								Input_TextBox.Clear();
							}
						}
						else
                        {
							MessageBox.Show("Cay da dat chieu cao toi da");
                        }
					
					}
					else
                    {
						MessageBox.Show("Gia tri nhap vao phai nam trong khoang tu -99 den 999 ");
					}

				}
				catch
				{
					MessageBox.Show(" Gia tri nhap khong dung");
				}
			}
		}

		private void Random_Button_Click(object sender, EventArgs e)
		{
			int N_Temp = 0;         // bien tang so lan random thanh cong de so sanh voi gia tri so lan random duoc chon
			Random ran = new Random();
			RandomList.Clear();
			int StatusInsert;
			if (Random_NumericUpDown.Value > 0)
			{
				while (N_Temp < Random_NumericUpDown.Value)
				{
					if (isBreak)
                    {
						isBreak = false;
						break;
                    }
					Way.Clear(); 
					if (treeHeight < 6)
                    {
						int value = ran.Next(100);

						StatusInsert = InsertNode(ref Root, value);
						if (StatusInsert != 0)
						{
							N_Temp++;
							RandomList.Add(value);
							Xd_ViTriCu(ref Root);
							Xd_ViTriMoi(ref Root);
							XacDinhTocDo();
							for (int i = 0; i < Speed; i++)
							{
								DiChuyenCay(ref Root);
								g.Clear(Color.White);
								VeCay_normal(Root);
								Thread.Sleep(1);
								Application.DoEvents();
							}
							g.Clear(Color.White);
							Xd_ViTri(ref Root);
							VeCay_normal(Root);
							XacDinhSoPhanTu();
							Input_TextBox.Clear();
						}
					}
					else
                    {
						MessageBox.Show("Cay da dat chieu cao toi da");
						break;
					}

				}
			}
		}

		private void stopButton_Click(object sender, EventArgs e)
		{
			isBreak = true;
		}
		private void stopButton_MouseClick(object sender, MouseEventArgs e)
		{
			//isBreak = true;
		}

		private void Main_PictureBox_MouseMove(object sender, MouseEventArgs e)
		{
			 MyMouseMove(Root, e.Location);
		}
		private void MyMouseMove(class_node node, PointF p)
		{
			if (node != null && ((p.X >= node.vitri.X  && p.X <= node.vitri.X + 34) && (p.Y >= node.vitri.Y && p.Y <= node.vitri.Y + 34)))
			{
				ShowTextBox(node);
				return;
			}
			if (node != null)
			{
				MyMouseMove(node.left, p);
				MyMouseMove(node.right, p);
			}

		}
		
		private void Main_PictureBox_MouseClick(object sender, MouseEventArgs e)
		{
			if (Root != null && e.Button == MouseButtons.Right)
			{
				RightClick(Root, e.Location);
			}
		}
		private void RightClick(class_node node, PointF p)
		{
			if (node != null && ((p.X >= node.vitri.X && p.X <= node.vitri.X + 34) && (p.Y >= node.vitri.Y && p.Y <= node.vitri.Y + 34)))
			{
				Delete_ContextMenuStrip.Show();
				Delete_ContextMenuStrip.Top = Convert.ToInt32(p.X);
				Delete_ContextMenuStrip.Left = Convert.ToInt32(p.Y-50);
				
				g.Clear(Color.White);
				VeCay_normal(Root);
				DrawNodeRed(node);
				Node_Temp = node;
				return;
			}
			if (node != null)
			{
				RightClick(node.left, p);
				RightClick(node.right, p);
			}
		}

		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{

			if (Root != null)
			{
				switch (e.KeyCode)
				{
					case Keys.Escape:
					Main_PictureBox.Controls.Remove(Info_RichTextBox);
					g.Clear(Color.White);
					VeCay_normal(Root);
					return;
				}
			}
		}
		private void Delete_ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (Root != null)
			{
				DelNode_Special(ref Root,Node_Temp.number);
				Xd_ViTriCu(ref Root);
				Xd_ViTriMoi(ref Root);
				XacDinhTocDo();
				for (int i = 0; i < Speed; i++)
				{
					DiChuyenCay(ref Root);
					g.Clear(Color.White);
					VeCay_normal(Root);
					Thread.Sleep(1);
					Application.DoEvents();
				}
				Node_Temp = null;
				g.Clear(Color.White);
				Xd_ViTri(ref Root);
				VeCay_normal(Root);
				XacDinhSoPhanTu();
				Main_PictureBox.Controls.Remove(Info_RichTextBox);
			}
			else
			{
				MessageBox.Show("Cay rong ! ");
			}
		}
        #endregion

        private void Input_TextBox_TextChanged(object sender, EventArgs e)
        {

        }


    }
}





