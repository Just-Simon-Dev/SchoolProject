import axios from "axios"
import { env } from "../../env/env"

export class StudentFormService {
	constructor(nameAndSurname, studentClass, date, numberOfHours, currentHour, issues){
		this.nameAndSurname = nameAndSurname
		this.studentClass = studentClass
		this.date = date
		this.numberOfHours = numberOfHours
		this.currentHour = currentHour
		this.issues = issues
	}
	PostData() {
		const url = env.apiUrl + 'StudentForm'

		const payload = {
			nameAndSurname: this.nameAndSurname,
			studentClass: this.studentClass,
			date: this.date,
			numberOfHours: this.numberOfHours,
			currentHour: this.currentHour,
			issues: this.issues
		}

		axios.post(url, payload).then(response => {
			console.log(response.data)
		}).catch(error => console.error(error))
	}
}