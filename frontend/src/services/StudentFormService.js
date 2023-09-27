import axios from "axios"
import { env } from "../../env/env"

export class StudentFormService {
	constructor(name, surname, studentClass, numberOfHours, currentHour, issues){
		this.name = name
		this.surname = surname
		this.studentClass = studentClass
		this.numberOfHours = numberOfHours
		this.currentHour = currentHour
		this.issues = issues
	}
	PostData() {
		const url = env.apiUrl + 'evidention/add-record'

		const payload = {
			name: this.name,
			surname: this.surname,
			class: this.studentClass,
			numberOfLessons: this.numberOfHours,
			lessonsTimes: this.currentHour,
			issues: this.issues
		}

		axios.post(url, payload).then(response => {
			console.log(response.data)
		}).catch(error => console.error(error))
	}
}