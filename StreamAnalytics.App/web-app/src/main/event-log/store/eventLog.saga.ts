import { takeLatest, call, put } from "redux-saga/effects";
import * as actions from "main/event-log/store/eventLog.actions";
import eventLogService from "main/event-log/services/eventLog.service";
import { IEventLogModel } from "../models/eventLog.model";

export const getEventLogs = function* () {
  console.log("Called getEventLogs");
  const logs: IEventLogModel = yield call(eventLogService.getFakeEventLog);
  yield put(actions.setEventLogs(logs));
  //return logs;
};

export const watchGetEventLogs = function* () {
  yield takeLatest(actions.GET_EVENT_LOGS, getEventLogs);
};
