


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
        private double _offsetDistance = 15;   // 相聚
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
                OnPropertyChanged(nameof(arrowAngle));
            }
            if (e.PropertyName == "y")
            {
                OnPropertyChanged(nameof(sourceStartPosY));
                OnPropertyChanged(nameof(targetStartPosY));
                OnPropertyChanged(nameof(arrowAngle));
            }
        }


        private double rawSourceX {
            get
            {
                return _sourceNode.x + _sourceNode.width/2.0;
            }
        }
        private double rawSourceY
        {
            get
            {
                return _sourceNode.y + _sourceNode.height / 2.0;
            }
        }

        private double rawTargetX {
            get
            {
                return _targetNode.x + _targetNode.width / 2.0;
            }
        }
        private double rawTargetY
        {
            get
            {
                return _targetNode.y + _targetNode.height / 2.0;
            }
        }




        /// <summary>
        /// 线
        /// </summary>
        public double sourceStartPosX {
            get => GetEdgePoint(_sourceNode,baseAngle).X;
        }



        /// <summary>
        /// 线
        /// </summary>
        public double sourceStartPosY
        {
            get => GetEdgePoint(_sourceNode, baseAngle).Y;
        }



        /// <summary>
        /// 线
        /// </summary>
        public double targetStartPosX
        {
            get => GetEdgePoint(_targetNode,baseAngle + Math.PI).X;
        }



        /// <summary>
        /// 线
        /// </summary>
        public double targetStartPosY
        {
            get => GetEdgePoint(_targetNode, baseAngle + Math.PI).Y;
        }



        /// <summary>
        /// 基础角度
        /// </summary>
        public double baseAngle
        {
            get
            {
                double dx = rawTargetX - rawSourceX;
                double dy = rawTargetY - rawSourceY;
                double delte = Math.Atan2(dy, dx);
                return delte;
            }
        }



        /// <summary>
        /// 
        /// </summary>
        public double arrowAngle
        {
            get => (baseAngle/ Math.PI) * 180;
        }


        private (double X, double Y) GetEdgePoint(NodeControlViewModel node, double angle)
        {
            double hw = node.width / 2.0;
            double hh = node.height / 2.0;
            double centerX = node.x + hw;
            double centerY = node.y + hh;
            if (hw == 0 || hh == 0) return (centerX, centerY);
            double absSin = Math.Abs(Math.Sin(angle));
            double absCos = Math.Abs(Math.Cos(angle));
            double distanceToEdge;

            // 决定在上面 还是 左右
            // 如果是左右的话 宽是定的
            // 如果是上下的话 高是定的
            if (absSin * hw > absCos * hh)
            {
                distanceToEdge = hh / absSin;
            }
            else
            {
                distanceToEdge = hw / absCos;
            }
            double totalDistance = distanceToEdge + _offsetDistance;
            return (centerX + Math.Cos(angle) * totalDistance,
                    centerY + Math.Sin(angle) * totalDistance);
        }




        /// <summary>
        /// 默认构造
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
