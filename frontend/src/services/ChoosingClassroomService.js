import axios from "axios"
import { env } from "../../env/env"

export class ChoosingClassroomService {
	getData() {
		const url = env.apiUrl + 'GetClassrooms'

		axios.get(url).then(response => {
			return response.data
		}).catch(error => {
			console.error(error)
		})
	}
	postData(classroom){
		const url = env.apiUrl + 'PostClassroom'

		const payload = {
			classroom: classroom
		}

		axios.post(url, payload).then(response => {
			return response.data
		}).catch(error => {
			console.error(error)
		})
	}
}