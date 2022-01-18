import { IAppState } from "global/store";
import { connect } from "react-redux";
import React from "react";
import { IEventDataModel, IEventLogModel } from "../models/eventLog.model";
import * as actions from "../store/eventLog.actions";
import { getEventLogs } from "../store/eventLog.state";

type IDispatchToProps = ReturnType<typeof mapStateToProps>;
type IMapToProps = typeof mapDispatchToProps;
interface IProps extends IDispatchToProps, IMapToProps {}

class EventLogComponent extends React.Component<IProps> {
  renderRow = (data: IEventDataModel, index: number) => {
    return (
      <tr key={index}>
        <td>{data.timestamp}</td>
        <td>{data.tagName}</td>
        <td>{data.assetName}</td>
        <td>{data.streamType}</td>
        <td>{data.state}</td>
        <td>{data.description}</td>
        <td>{data.source}</td>
        <td>{data.severity}</td>
        <td>{data.quality}</td>
      </tr>
    );
  };

  public componentDidMount() {
    const { getEventLogs } = this.props;
    console.log("Component mounted");
    getEventLogs();
  }

  render() {
    const { eventLogs } = this.props;
    return (
      <div>
        This is event log page.
        <table className="event-log-table">
          <tbody>
            {eventLogs.events.map((data, i) => this.renderRow(data, i))}
          </tbody>
        </table>
      </div>
    );
  }
}

const mapStateToProps = (state: IAppState) => ({
  eventLogs: getEventLogs(state),
});

const mapDispatchToProps = {
  getEventLogs: actions.getEventLogs,
};

export const EventLog = connect(
  mapStateToProps,
  mapDispatchToProps
)(EventLogComponent);
