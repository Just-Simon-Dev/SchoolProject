import axios from "axios"
import { env } from "../../env/env"

export class ChoosingClassroomService {
	getData() {
		const url = env.apiUrl + 'authentication/get-classroom'

		axios.get(url).then(response => {
			return response.data
		}).catch(error => {
			console.error(error)
		})
	}
	postData(classroom){
		const url = env.apiUrl + 'authentication/set-classroom-to-user'

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