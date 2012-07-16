using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.VisualStudio.DebuggerVisualizers;

namespace DebuggerAttribute
{

    [DebuggerVisualizer("User")]
    public partial class UserDebugViewer : Form
    {
        public UserDebugViewer(User user)
        {
            InitializeComponent();

            tb_Name.Text = user.Name;
            tb_Title.Text = user.Title;
            pb_Picture.Image = user.Picture;
        }
    }
}
