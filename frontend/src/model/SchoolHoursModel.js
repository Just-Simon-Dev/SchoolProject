export class SchoolHoursModel {
	constructor(breakHours, breakMinutes, endLessonHours, endLessonMinutes) {
		this.breakHours = breakHours
		this.breakMinutes = breakMinutes
		this.endLessonHours = endLessonHours
		this.endLessonMinutes = endLessonMinutes
	}

	getBreakDecimal() {
		return this.breakHours + this.breakMinutes / 60
	}

	getEndLessonDecimal() {
		return this.endLessonHours + this.endLessonMinutes / 60 
	}
}