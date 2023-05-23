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

	let name = "";
	let surname = "";
	let studentClass = "";
	let date = mm + ' / ' + dd + ' / ' + yyyy;
	let numberOfHours = ref(1);
	let currentHour = computed(() => getCurrentHour(numberOfHours.value))
	let issues = ""
	let error = ref('')

	function sendData(){
		if(name == "" || surname == "" || studentClass == "" || currentHour.value == ""){
			console.log(error)
			error.value = "niektóre pola nie zostały wypełnione!"
			console.log(error)
			return;
		}
		const studentFormService = new StudentFormService(`${name} ${surname}`, studentClass, date, numberOfHours, currentHour.value, issues)
		studentFormService.PostData()
		window.close()
	}

	

</script>

<template>
	<div class="flex flex-col w-full min-h-screen items-center justify-center bg-slate-50">
		<div class="relative flex items-end bg-white p-10 rounded-lg shadow-md space-x-10 overflow-hidden">
			<div class="flex items-end h-full z-10">
				<img class="align-bottom" src="logo.svg" alt="logo ZSŁ">
			</div>
			<form class="grid grid-cols-2 justify-center gap-10 rounded-lg shadow-md p-10 bg-[#203354] text-white z-10">
				<div class="flex flex-col space-y-2">
					<div>
						<p class="p-1">Imię</p>
						<input
						class="rounded focus:outline-none text-black py-1 px-2"
						type="text" 
						:value="name" 
						@input="event => name = event.target.value" 
						required
						/>
					</div>
					<div>
						<p class="p-1">Nazwisko</p>
						<input
						class="rounded focus:outline-none text-black py-1 px-2"
						type="text" 
						:value="surname" 
						@input="event => surname = event.target.value" 
						required
						/>
					</div>
					<div>
						<p class="p-1">Klasa</p>
						<input
						class="rounded focus:outline-none text-black py-1 px-2"
						type="text" 
						:value="studentClass" 
						@input="event => studentClass = event.target.value"
						required 
						/>
					</div>
					<div>
						<p class="p-1">Liczba lekcji</p>
						<input
						class="rounded focus:outline-none text-black py-1 px-2"
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
						<p class="p-1">Uwagi</p>
						<textarea
						class="rounded focus:outline-none text-black py-1 px-2 resize-none"
						rows="3"
						type="text" 
						:value="issues" 
						@input="event => issues = event.target.value" 
						></textarea>
					</div>
				</div>
				<div>
					<div>
						<p class="p-1">Data</p>
						<input
						class="rounded focus:outline-none text-black py-1 px-2"
						type="text" 
						:value="date" 
						@input="event => date = event.target.value" 
						disabled
						/>
					</div>
					<div>
						<p class="p-1">Godzina</p>
						<input
						class="rounded focus:outline-none text-black py-1 px-2"
						type="text" 
						:value="currentHour"
						disabled
						/>
					</div>
				</div>
				<div class="flex justify-center col-span-2">
					<button type="button" class="w-64 tracking-wider uppercase bg-[#da1c04] rounded-full py-2" v-on:click="sendData()">Wyślij</button>
				</div>
			</form>
			<div class="absolute -bottom-40 -right-36 rounded-full w-96 h-96 bg-[#da1c04] z-0"></div>
			<div class="absolute -top-40 left-12 rounded-full w-96 h-96 bg-[#da1c04] z-0 shadow"></div>
		</div>
		<div class="error" v-if="error != ''">
			{{ error }}
		</div>
	</div>
</template>

<style scoped>

</style>