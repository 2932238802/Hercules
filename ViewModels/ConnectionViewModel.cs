


using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Hercules.ViewModels
{
    public class ConnectionViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        // 内部 存放 两个节点
        // 用于 绑定两个 属性
        private NodeControlViewModel _sourceNode;   // 
        private NodeControlViewModel _targetNode;   // 目前

        protected void OnPropertyChanged([CallerMemberName] string?name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name)); 
        }


        /// <summary>
        /// node 发生改变 对应线的位置 也要改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Node_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "x")
            {
                OnPropertyChanged(nameof(sourceStartPosX));
                OnPropertyChanged(nameof(targetStartPosX));
            }
            if (e.PropertyName == "y")
            {
                OnPropertyChanged(nameof(sourceStartPosY));
                OnPropertyChanged(nameof(targetStartPosY));
            }
        }



        /// <summary>
        /// 
        /// </summary>
        public double sourceStartPosX {
            get => _sourceNode.x + 40;
        }



        /// <summary>
        /// 
        /// </summary>
        public double sourceStartPosY
        {
            get => _sourceNode.y + 40;
        }



        /// <summary>
        /// 
        /// </summary>
        public double targetStartPosX
        {
            get => _targetNode.x + 20;
        }



        /// <summary>
        /// 
        /// </summary>
        public double targetStartPosY
        {
            get => _targetNode.y + 20;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="source_node"></param>
        /// <param name="target_node"></param>
        public ConnectionViewModel(NodeControlViewModel source_node
                , NodeControlViewModel target_node)
        {
            _sourceNode = source_node;
            _targetNode = target_node;

            _sourceNode.PropertyChanged += Node_PropertyChanged;
            _targetNode.PropertyChanged += Node_PropertyChanged;
        }


    }
}
