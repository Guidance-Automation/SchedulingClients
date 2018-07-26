using SchedulingClients.AgentAttentionServiceReference;
using System.Windows;
using System.Windows.Controls;
using System.Linq;

namespace SchedulingClients.UserControls
{
  /// <summary>
  /// Interaction logic for AgentAttentionClientControl.xaml
  /// </summary>
  public partial class AgentAttentionClientControl : UserControl
	{
		public AgentAttentionClientControl()
		{
	      InitializeComponent();
    }
        
    private void AgentAttentionClient_AgentAttentionChange(AgentAttentionData[] agentAttentionData)
    {
#warning needs sanity check
            Application.Current.Dispatcher.Invoke(() =>
            agentAttentionDataGrid.ItemsSource = agentAttentionData
        );
    }
	
    private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        IAgentAttentionClient agentAttentionClient = DataContext as IAgentAttentionClient;
        agentAttentionClient.AgentAttentionChange += AgentAttentionClient_AgentAttentionChange;
    }
  }
}
