module.exports = function(grunt) {
	grunt.initConfig({ // setup tasks and options for tasks
		pkg: grunt.file.readJSON('package.json'),
		sass: {
			options: {
					sourcemap: 'none', // disable source mapping comments for css and scss files
					style: 'compressed',
				},
			dist: {
				files: { // set directory for compiled css
					'proof-of-concept-spa.Web/Scripts/css/site.css': 'proof-of-concept-spa.Web/Scripts/css/site.scss'
				}
			},
			dev: {
				options: {
					style: 'expanded',
					sourcemap: 'none'
				},
				files: {
					'proof-of-concept-spa.Web/Scripts/css/site.css': 'proof-of-concept-spa.Web/Scripts/css/site.scss'
				}
			}
		},
		watch: { // can run individual tasks from command line e.g. grunt watch, grunt sass
			css: {
				files: '**/*.scss', // watch all .scss files
				tasks: ['sass']
			}
		}
	});
	
	grunt.loadNpmTasks('grunt-contrib-sass');
	grunt.loadNpmTasks('grunt-contrib-watch');
	
	grunt.registerTask('build', ['sass:dist']); // run grunt build from command line
	grunt.registerTask('default',['sass:dev', 'watch']); // run grunt from command line
};