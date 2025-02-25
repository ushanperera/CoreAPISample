using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Infrastructure.Contract.Auth
{
    public interface IParameterHelper
    {
        bool ShowStrategyQuickUpdate { get; }
        bool ShowAuditModule { get; }
        bool ShowPESModule { get; }
        bool ShowRiskModule { get; }
        bool ShowIPMModule { get; }
        bool ShowRiskProfessional { get; }
        bool ShowHazzardModule { get; }
        bool ShowIncidentModule { get; }
        bool ShowComplianceModule { get; }
        bool ShowHideCompleted { get; }
        bool CheckHideCompleted { get; }
        bool HideProjectRiskFromRiskModule { get; }
        bool IsShowRiskModule { get; }
        Guid ActionStatusHiddenIdentifier { get; }
    }
}
