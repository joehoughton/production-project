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
                    'production-project.Web/Scripts/css/site.css': 'production-project.Web/Scripts/css/site.scss'
                }
            },
            dev: {
                options: {
                    style: 'expanded',
                    sourcemap: 'none'
                },
                files: {
                    'production-project.Web/Scripts/css/site.css': 'production-project.Web/Scripts/css/site.scss'
                }
            }
        },
        watch: { // can run individual tasks from command line e.g. grunt watch, grunt sass
            css: {
                files: '**/*.scss', // watch all .scss files
                tasks: ['sass']
            },
            scripts: {
                files: ['production-project.Web/Scripts/app/**/*.js', 'production-project.Web/Scripts/plugins/**/*.js'],
                tasks: ['jshint:dev', 'jscs:dev'],
                options: {
                    spawn: false // speeds up the reaction time of the watch
                }
            },
        },
        jshint: {
            dist: {
                options: {
                    jshintrc: '.jshintrc',
                    reporter: require('jshint-stylish'),
                    force: false,
                    ignores: ['production-project.Web/Scripts/lib/**']
                },
                src: ['production-project.Web/Scripts/**/*.js'],
            },
            dev: {
                options: {
                    jshintrc: '.jshintrc',
                    reporter: require('jshint-stylish'),
                    force: true,
                    ignores: ['production-project.Web/Scripts/lib/**']
                },
                src: ['production-project.Web/Scripts/**/*.js'],
            }
        },

        jscs: {
            dist: {
                src: 'production-project.Web/Scripts/**/*.js',
                options: {
                    config: '.jscsrc',
                    verbose: true,
                    force: false,
                    reporter: require('jscs-stylish').path
                }
            },
            dev: {
                src: 'production-project.Web/Scripts/**/*.js',
                options: {
                    config: '.jscsrc',
                    verbose: true,
                    force: true,
                    reporter: require('jscs-stylish').path
                }
            }
        },
        bower: {
            install: {
                options: {
                    targetDir: './bower_components'
                }
            }
        },
		copy: {
			  main: {
				files: [
				  {
					nonull: true,
					expand: true,
					cwd: 'production-project.Web/Scripts/lib/bower_components/bootstrap-sass-official/assets',
					src: ['fonts/**'],
					dest: 'production-project.Web/Scripts/'
				  }
				]
			  }
		},
    });

    grunt.loadNpmTasks('grunt-contrib-sass');
    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.loadNpmTasks('grunt-contrib-jshint');
    grunt.loadNpmTasks('grunt-jscs');
	grunt.loadNpmTasks('grunt-bower-task');
	grunt.loadNpmTasks('grunt-contrib-copy');
	
    grunt.registerTask('build', ['bower:install', 'copy', 'sass:dist', 'jshint:dist', 'jscs:dist']); // run grunt build from command line - production
    grunt.registerTask('default', ['sass:dev', 'watch', 'jshint:dev', 'jscs:dev']); // run grunt from command line - development
};