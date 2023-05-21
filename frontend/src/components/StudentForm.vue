<script setup>

	import { SchoolHoursModel } from '../model/SchoolHoursModel'
	import { StudentFormService } from '../services/StudentFormService'
	import { ref, computed } from 'vue';

	const schoolHours = [
		new SchoolHoursModel(7, 20, 8, 5),
		new SchoolHoursModel(8, 5, 9, 0),
		new SchoolHoursModel(9, 0, 9, 55),
		new SchoolHoursModel(9, 55, 10, 50),
		new SchoolHoursModel(10, 50, 11, 45),
		new SchoolHoursModel(11, 45, 12, 50),
		new SchoolHoursModel(12, 50, 13, 45),
		new SchoolHoursModel(13, 45, 14, 40),
		new SchoolHoursModel(14, 40, 15, 35),
		new SchoolHoursModel(15, 35, 16, 30),
		new SchoolHoursModel(16, 30, 17, 25)
	]

	let numberOfLessonLeft = 2;
	const today = new Date();
	const yyyy = today.getFullYear();
	let mm = today.getMonth() + 1; // Months start at 0!
	let dd = today.getDate();

	if (dd < 10) dd = '0' + dd;
	if (mm < 10) mm = '0' + mm;

	let getCurrentHour = (numOfHours) => {

		let output = []
		let index = 0

		let currentTime = today.getHours() + today.getMinutes() / 60
		
		schoolHours.forEach(element => {
			let breakDecimal = element.getBreakDecimal()
			let endLessonDecimal = element.getEndLessonDecimal()

			let isBetweenThisLesson = currentTime >= breakDecimal && currentTime <= endLessonDecimal
			
			if(isBetweenThisLesson){
				output.push(element)

				numberOfLessonLeft = schoolHours.length - schoolHours.indexOf(element)

				index = schoolHours.indexOf(element) + Number(numOfHours) - 1
				output.push(schoolHours[index])
			}
		});

		if(output.length < 2) return "";

		return `${output[0].breakHours}:${output[0].breakMinutes} - ${output[1].endLessonHours}:${output[1].endLessonMinutes}`;
	}

	let nameAndSurname = "";
	let studentClass = "";
	let date = mm + ' / ' + dd + ' / ' + yyyy;
	let numberOfHours = ref(1);
	let currentHour = computed(() => getCurrentHour(numberOfHours.value))
	let issues = ""
	let error = ref('')

	function sendData(){
		if(nameAndSurname == "" || studentClass == "" || currentHour.value == ""){
			console.log(error)
			error.value = "niektóre pola nie zostały wypełnione!"
			console.log(error)
			return;
		}
		const studentFormService = new StudentFormService(nameAndSurname, studentClass, date, numberOfHours, currentHour.value, issues)
		studentFormService.PostData()
		window.close()
	}

	

</script>

<template>
	<form>
		<div>
			imie i nazwisko 
			<input
			type="text" 
			:value="nameAndSurname" 
			@input="event => nameAndSurname = event.target.value" 
			required
			/>
		</div>
		<div>
			klasa 
			<input
			type="text" 
			:value="studentClass" 
			@input="event => studentClass = event.target.value"
			required 
			/>
		</div>
		<div>
			data 
			<input
			type="text" 
			:value="date" 
			@input="event => date = event.target.value" 
			disabled
			/>
		</div>
		<div>
			liczba lekcji
			<input
			type="number" 
			:value="numberOfHours" 
			@input="event => {
				if(event.target.value < 0){
					numberOfHours = 0
				} 
				if(event.target.value > numberOfLessonLeft){
					numberOfHours = numberOfLessonLeft
				}
				else{
					numberOfHours = event.target.value
				}
			}"
			min="0"
			:max="numberOfLessonLeft"
			required
			/>
		</div>
		<div>
			godzina
			<input
			type="text" 
			:value="currentHour"
			disabled
			/>
		</div>
		<div>
			uwagi
			<input
			type="text" 
			:value="issues" 
			@input="event => issues = event.target.value" 
			/>
		</div>
		<button type="button" v-on:click="sendData()">send</button>
	</form>
	<div class="error" v-if="error != ''">
		{{ error }}
	</div>
</template>

<style scoped>

</style>