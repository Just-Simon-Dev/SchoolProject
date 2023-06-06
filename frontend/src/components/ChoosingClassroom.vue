<script setup>

import { ChoosingClassroomService } from '../services/ChoosingClassroomService'

let classrooms = new ChoosingClassroomService().getData()

let sendData = (classroom) => {
	console.log(classroom)
	new ChoosingClassroomService().postData(classroom)
}

</script>

<template>
	<button v-for="classroom of classrooms" v-bind:key="classroom" v-on:click="sendData(classroom)">{{ classroom }}</button>
</template>

<style scoped>

</style>