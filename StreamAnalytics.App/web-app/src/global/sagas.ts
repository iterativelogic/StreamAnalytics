import * as eventLogSagas from "main/event-log/store/eventLog.saga";
import { all } from "redux-saga/effects";

export const rootSaga = function* () {
  yield all([eventLogSagas.watchGetEventLogs()]);
};
