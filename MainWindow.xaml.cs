using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Hercules.ViewModels;
using Hercules.Views;
namespace Hercules;


public partial class MainWindow : Window
{
    private bool _isDragging = false;
    private Point _lastMousePosition;               // 记录上一次 鼠标的位置
    private NodeControlViewModel? _draggedNode;     // 记录 正在拖动的 节点

    // 对于集合中的每一个 NodeControlViewModel，
    // 它都会根据 ItemTemplate 里的指示，实时 new 出一个你的自定义控件实例
    public ObservableCollection<NodeControlViewModel> MainWindow_nodes
    {
        get;
        set;
    } = new ObservableCollection<NodeControlViewModel>();

    public ObservableCollection<ConnectionViewModel> MainWindow_connections{
        get;
        set;
    } = new ObservableCollection<ConnectionViewModel>();

    /// <summary>
    /// 初始化
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();
        this.DataContext = this;
        NodeControlViewModel node1 = new NodeControlViewModel { x = 100, y = 100, title = "节点一" };
        NodeControlViewModel node2 = new NodeControlViewModel { x = 300, y = 200, title = "节点二" };
        MainWindow_nodes.Add(node1);
        MainWindow_nodes.Add(node2);
        ConnectionViewModel cvm = new ConnectionViewModel(node1, node2);
        MainWindow_connections.Add(cvm);    
    }
}