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
		int The_Height_Tree = 0;

		public class_node Root;
		public class_node Node_Temp; // node dung de dung tam khi bat su kien click Delete_ContextMenuStrip
		int Speed;
		Bitmap bitmap;
		Graphics g;
		RichTextBox Info_RichTextBox = new RichTextBox();
		RichTextBox Compare_RichTextBox = new RichTextBox();
		List<int> Way = new List<int>();
		List<int> RandomList = new List<int>();

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

		private void XacDinhTocDo()
		{
			if (Speed_ComboBox.Text != "1" && Speed_ComboBox.Text != "2" && Speed_ComboBox.Text != "3" && Speed_ComboBox.Text != "4")
			{
				Speed_ComboBox.Text = "4";
			}
			Speed = (5 - Convert.ToInt32(Speed_ComboBox.Text)) * (int)(10);
		}
		#endregion

		#region Build Tree 




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
							return 1;
					}

				}
			}
			return 2;
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
			The_Height_Tree = High(Root) - 1;
			if (The_Height_Tree == -1)
			{
				The_Height_Tree = 0;
			}

			if (Total_Node == 0 || Total_Node == 1)
			{
				Total_Intermediate_Node = 0;
			}
			The_Height_Tree = High(Root) - 1;
			if (The_Height_Tree == -1)
			{
				The_Height_Tree = 0;
			}

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

		#endregion

		#region Control 
		private void findButton_Click(object sender, EventArgs e)
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

		//  xoa toan cay 
		private void Del_Tree_Button_Click(object sender, EventArgs e)
		{
			g.Clear(Color.White);
			Main_PictureBox.Image = bitmap;
			Main_PictureBox.Controls.Remove(Info_RichTextBox);
			MessageBox.Show(" Da xoa thanh cong cay! ");
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
						if (The_Height_Tree <= 5) {
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
									g.Clear(Color.White);
									VeCay_normal(Root);
									Thread.Sleep(2);
									Application.DoEvents();
								}
								g.Clear(Color.White);
								Xd_ViTri(ref Root);
								VeCay_normal(Root);
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
					if (The_Height_Tree < 6)
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
								g.Clear(Color.White);
								VeCay_normal(Root);
								Thread.Sleep(1);
								Application.DoEvents();
							}
							g.Clear(Color.White);
							Xd_ViTri(ref Root);
							VeCay_normal(Root);
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

        #endregion


    }
}





