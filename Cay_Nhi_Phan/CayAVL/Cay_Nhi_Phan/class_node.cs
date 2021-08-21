using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Cay_Nhi_Phan
{
		public class class_node
		{
			public int canbang;     // Hệ số cân bằng
			public int number;      // Giá trị node
			public class_node left;       // Node pLeft
			public class_node right;      // Node pRight
			public PointF vitri;    // Vị trí hiện tại của node
			public PointF locationOld;    // Vị trí cũ - dùng cho việc di chuyển cây
			public PointF locationNew;    // Vị trí mới - dùng cho việc di chuyển cây
										  // Hàm tạo không đối số
			public class_node()
			{
				this.canbang = 0;
				this.left = null;
				this.right = null;
				this.vitri.X = 512;
				this.vitri.Y = 20;
				this.locationOld.X = 512;
				this.locationOld.Y = 20;
			}
			// Hàm tạo có đối số là giá trị node
			public class_node(int number)
			{
				this.canbang = 0;
				this.number = number;
				this.left = null;
				this.right = null;
				this.vitri.X = 512;
				this.vitri.Y = 20;
				this.locationOld.X = 512;
				this.locationOld.Y = 20;
			}
		}
}
